#include "KvntUsbDrvr.h"

#ifdef ALLOC_PRAGMA
#pragma alloc_text(PAGE, RegOpenDeviceSubKey)
#pragma alloc_text(PAGE, RegReadDword)
#pragma alloc_text(PAGE, RegReadString)
#endif

  // открытие ключа реестра, хранящего параметры нашего фильтра
NTSTATUS RegOpenDeviceSubKey(IN PDEVICE_OBJECT pDeviceObj, IN PUNICODE_STRING pSubKeyName, OUT PHANDLE phKey)
{
    HANDLE hKeyRoot;

	  // открываем ключ устройства
    NTSTATUS status = IoOpenDeviceRegistryKey(pDeviceObj, PLUGPLAY_REGKEY_DEVICE, KEY_READ, &hKeyRoot);
    
    if (NT_SUCCESS(status))
    {
        OBJECT_ATTRIBUTES Attributes;

		  // создаем объект аттрибутов для открытия требуемого вложенного ключа
        InitializeObjectAttributes(&Attributes, pSubKeyName, OBJ_KERNEL_HANDLE | OBJ_CASE_INSENSITIVE, hKeyRoot, NULL);

		  // открываем вложенный ключ, ключ устройства закрываем
        status = ZwOpenKey(phKey, KEY_READ, &Attributes);

        ZwClose(hKeyRoot);
    }
    return status;
}

  // чтение DWORD'а
NTSTATUS RegReadDword(IN HANDLE hKey, IN PUNICODE_STRING pValueName, OUT PULONG pResult)
{
    NTSTATUS status = STATUS_UNSUCCESSFUL;
    ULONG    Length;

	  // очищаем память
    RtlZeroMemory(pResult, sizeof(*pResult));
    
	  // определяем объем памяти, необходимой для считывания
    status = ZwQueryValueKey(hKey, pValueName, KeyValuePartialInformation, NULL, 0, &Length);
    
    if (status != STATUS_OBJECT_NAME_NOT_FOUND && Length > 0)
      {
		  // выделяем память для считывания
        PKEY_VALUE_PARTIAL_INFORMATION Buffer = (PKEY_VALUE_PARTIAL_INFORMATION)ExAllocatePool(PagedPool, Length);

        if (!Buffer) status = STATUS_INSUFFICIENT_RESOURCES; 
          else
            {
				// читаем значение параметра
              status = ZwQueryValueKey(hKey, pValueName, KeyValuePartialInformation, Buffer, Length, &Length);
            
			    // проверяем тип считанного параметра
              if (Buffer->Type != REG_DWORD) status = STATUS_OBJECT_NAME_NOT_FOUND;
				
				// копируем в выходной буфер и освобождаем выделенную память
              if (NT_SUCCESS(status)) RtlCopyMemory(pResult, Buffer->Data, Buffer->DataLength);
            
              ExFreePool(Buffer);
            } 
      }
    return status;
}

  // чтение строки
NTSTATUS RegReadString(IN HANDLE hKey, IN PUNICODE_STRING pValueName, OUT PUNICODE_STRING pResult)
{
    NTSTATUS status = STATUS_UNSUCCESSFUL;
    ULONG    Length;

    RtlZeroMemory(pResult, sizeof(*pResult));
    
    status = ZwQueryValueKey(hKey, pValueName, KeyValuePartialInformation, NULL, 0, &Length);
    
    if (status != STATUS_OBJECT_NAME_NOT_FOUND && Length > 0)
      {
        PKEY_VALUE_PARTIAL_INFORMATION Buffer = (PKEY_VALUE_PARTIAL_INFORMATION)ExAllocatePool(PagedPool, Length);

        if (!Buffer) status = STATUS_INSUFFICIENT_RESOURCES; 
          else
            {
              status = ZwQueryValueKey(hKey, pValueName, KeyValuePartialInformation, Buffer, Length, &Length);
            
              if (Buffer->Type != REG_SZ) status = STATUS_OBJECT_NAME_NOT_FOUND;
            
              if (NT_SUCCESS(status))
                {
                  PVOID pBuffer = ExAllocatePool(PagedPool, Buffer->DataLength);
                
                  if (!pBuffer) 
					  status = STATUS_INSUFFICIENT_RESOURCES;
                    else
                      {
                        RtlCopyMemory(pBuffer, Buffer->Data, Buffer->DataLength);
                        RtlInitUnicodeString(pResult, (PCWSTR)pBuffer);
                        status = STATUS_SUCCESS;
                      }
                }            
              ExFreePool(Buffer);
            } 
      }
    return status;
}