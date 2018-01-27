#ifndef STRINGS_H
#define STRINGS_H

// имя основного FDO драйвера и его символьная ссылка
#define DEVICE_NAME L"\\Device\\KvntUsbDrvr_MainDev"
#define SYM_LINK	L"\\??\\KvntUsbDrvr_MainDevDriver"

#define NUMBER_LENGTH 10
#define NEW_LINE	  "\x0D\x0A"
#define SEMI		  ","

#define STR_ERROR_BUFFER_ALLOCATION_FAILED "Error during URB buffer allocation!\x0D\x0A"

#define FUNC_SELECT_INTERFACE					"F: SelectInterface\x0D\x0A"
#define FUNC_SELECT_CONFIGURATION				"F: SelectConfiguration\x0D\x0A"

// _URB_PIPE_REQUEST
#define FUNC_RESET_PIPE							"F: ResetPipe\x0D\x0A"
#define FUNC_ABORT_PIPE							"F: AbortPipe\x0D\x0A"

#define FUNC_GET_FRAME_LENGTH					"F: GetFrameLength\x0D\x0A"
#define FUNC_SET_FRAME_LENGTH					"F: SetFrameLength\x0D\x0A"
#define FUNC_GET_CURRENT_FRAME_NUMBER			"F: GetCurrentFrameNumber\x0D\x0A"
#define FUNC_CONTROL_TRANSFER					"F: ControlTransfer\x0D\x0A"
#define FUNC_BULK_OR_INTERRUPT_TRANSFER			"F: BulkOrInterruptTransfer\x0D\x0A"
#define FUNC_ISOCH_TRANSFER						"F: IsochronousTransfer\x0D\x0A"

// _URB_CONTROL_DESCRIPTOR_REQUEST
#define FUNC_GET_DESC_FROM_DEVICE				"F: GetDescriptorFromDevice\x0D\x0A"
#define FUNC_GET_DESC_FROM_ENDPOINT				"F: GetDescriptorFromEndpoint\x0D\x0A"
#define FUNC_GET_DESC_FROM_INTERFACE			"F: GetDescriptorFromInterface\x0D\x0A"
#define FUNC_SET_DESC_TO_DEVICE					"F: SetDescriptorToDevice\x0D\x0A"
#define FUNC_SET_DESC_TO_ENDPOINT				"F: SetDescriptorToEndpoint\x0D\x0A"
#define FUNC_SET_DESC_TO_INTERFACE				"F: SetDescriptorToInterface\x0D\x0A"

// _URB_CONTROL_GET_STATUS_REQUEST
#define FUNC_GET_STAT_FROM_DEVICE				"F: GetStatusFromDevice\x0D\x0A"
#define FUNC_GET_STAT_FROM_INTERFACE			"F: GetStatusFromInterface\x0D\x0A"
#define FUNC_GET_STAT_FROM_ENDPOINT				"F: GetStatusFromEndpoint\x0D\x0A"
#define FUNC_GET_STAT_FROM_OTHER				"F: GetStatusFromOther\x0D\x0A"

// _URB_CONTROL_FEATURE_REQUEST
#define FUNC_SET_FEAT_TO_DEVICE					"F: SetFeatureToDevice\x0D\x0A"
#define FUNC_SET_FEAT_TO_INTERFACE				"F: SetFeatureToInterface\x0D\x0A"
#define FUNC_SET_FEAT_TO_ENDPOINT				"F: SetFeatureToEndpoint\x0D\x0A"
#define FUNC_SET_FEAT_TO_OTHER					"F: SetFeatureToOther\x0D\x0A"
#define FUNC_CLEAR_FEAT_TO_DEVICE				"F: ClearFeatureToDevice\x0D\x0A"
#define FUNC_CLEAR_FEAT_TO_INTERFACE			"F: ClearFeatureToInterface\x0D\x0A"
#define FUNC_CLEAR_FEAT_TO_ENDPOINT				"F: ClearFeatureToEndpoint\x0D\x0A"
#define FUNC_CLEAR_FEAT_TO_OTHER				"F: ClearFeatureToOther\x0D\x0A"

#define FUNC_CONTROL_GET_INTERFACE_REQUEST		"F: ControlGetInterfaceRequest\x0D\x0A"
#define FUNC_CONTROL_GET_CONFIGURATION_REQUEST	"F: ControlConfigurationRequest\x0D\x0A"

  // строки для вывода информации о дескрипторе устройства
#define STR_DEVICE_DESCRIPTOR	"Device descriptor:\x0D\x0A"
#define STR_USB_VERSION			"  USB version                  : "
#define STR_USB_CLASS_CODE		"  USB class code               : "
#define STR_USB_SUBCLASS		"  USB subclass code            : "
#define STR_USB_PROTOCOL_CODE	"  USB protocol code            : "
#define STR_MAX_PACK_EP0		"  Max packet size for Endpoint0: "
#define STR_VENDOR_ID			"  Vendor ID                    : "
#define STR_PROD_ID				"  Product ID                   : "
#define STR_NUM_CONFIGS			"  Number of configurations     : "

  // строки для вывода информации о дескрипторе конфигурации
#define STR_CONFIG_DESCRIPTOR "Configuration descriptor:\x0D\x0A"
#define STR_NUM_INTERFACES    "  Num of interfaces : "
#define STR_CONFIG_VALUE      "  Config value      : "
#define STR_CONFIG_ATTRS	  "  Config attributes : "
#define STR_MAX_POWER		  "  MaxPower          : "

#define STR_CONFIG_HANDLE "Configuration handle: "

  // строки для вывода информации о интерфейсе
#define STR_INTERFACE_INFORMATION "Interface information:\x0D\x0A"
#define STR_INTERFACE_NUMBER      "  Interface number : "
#define STR_ALT_SETTING           "  Alternate setting: "
#define STR_CLASS			      "  Class            : "
#define STR_SUBCLASS              "  SubClass         : "
#define STR_PROTOCOL              "  Protocol         : "
#define STR_INTERFACE_HANDLE      "  Interface handle : "
#define STR_NUM_PIPES             "  Number of pipes  : "

  // строки для вывода информации о канале
#define STR_PIPE_INFORMATION	"Pipe information:\x0D\x0A"
#define STR_MAX_PACKET_SIZE		"  Max packet size      : "
#define STR_ENDPNT_ADDRESS		"  Endpoint address     : "
#define STR_INTERVAL			"  Interval             : "
#define STR_PIPE_TYPE			"  Pipe type            : "
#define STR_PIPE_HANDLE			"  Pipe handle          : "
#define STR_MAX_TRANSFER_SIZE	"  Maximum transfer size: "
#define STR_PIPE_FLAGS			"  Pipe flags           : "

  // типы каналов
#define STR_CONTROL_PIPE		"Control\x0D\x0A"
#define STR_ISOCHRONOUS_PIPE	"Isochronous\x0D\x0A"
#define STR_BULK_PIPE			"Bulk\x0D\x0A"
#define STR_INTERRUPT_PIPE		"Interrupt\x0D\x0A"
#define STR_UNKNOWN_PIPE		"Unknown\x0D\x0A"

  // строки для вывода информации о передаче данных BulkOrInterruptTransfer
#define STR_BoI_TRANSFER_PIPE_HANDLE	"  Pipe handle    : "
#define STR_TRANSFER_FLAGS				"  Transfer flags : "
#define STR_BUFFER_LENGTH				"  Buffer length  : "
#define STR_GENERIC_BUFFER				"  Generic buffer : "
#define STR_MDL_BUFFER					"  MDL buffer     :\x0D\x0A"

#endif