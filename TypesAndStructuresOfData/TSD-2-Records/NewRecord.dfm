object Form3: TForm3
  Left = 0
  Top = 0
  Caption = 'New record'
  ClientHeight = 283
  ClientWidth = 308
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 16
    Top = 24
    Width = 41
    Height = 13
    Caption = 'Caption:'
  end
  object Label2: TLabel
    Left = 16
    Top = 67
    Width = 61
    Height = 13
    Caption = 'Performance'
  end
  object Edit1: TEdit
    Left = 120
    Top = 24
    Width = 161
    Height = 21
    TabOrder = 0
  end
  object Edit2: TEdit
    Left = 120
    Top = 64
    Width = 161
    Height = 21
    TabOrder = 1
  end
  object GroupBox1: TGroupBox
    Left = 16
    Top = 112
    Width = 265
    Height = 105
    Caption = 'Performance type'
    TabOrder = 2
    object Label3: TLabel
      Left = 67
      Top = 25
      Width = 19
      Height = 13
      Caption = 'Age'
    end
    object Composer: TLabel
      Left = 67
      Top = 76
      Width = 48
      Height = 13
      Caption = 'Composer'
    end
    object Bevel1: TBevel
      Left = 61
      Top = 15
      Width = 4
      Height = 88
      Style = bsRaised
    end
    object RadioButton1: TRadioButton
      Left = 12
      Top = 24
      Width = 49
      Height = 17
      Caption = 'Child'
      TabOrder = 0
    end
    object RadioButton2: TRadioButton
      Left = 12
      Top = 49
      Width = 49
      Height = 17
      Caption = 'Adult'
      TabOrder = 1
    end
    object RadioButton3: TRadioButton
      Left = 12
      Top = 75
      Width = 49
      Height = 17
      Caption = 'Music'
      TabOrder = 2
    end
    object ComboBox1: TComboBox
      Left = 125
      Top = 46
      Width = 121
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 3
      Items.Strings = (
        'Play'
        'Drama'
        'Comedy')
    end
    object Edit3: TEdit
      Left = 125
      Top = 73
      Width = 121
      Height = 21
      TabOrder = 4
    end
    object Edit4: TEdit
      Left = 125
      Top = 19
      Width = 52
      Height = 21
      TabOrder = 5
      Text = '10'
    end
    object UpDown1: TUpDown
      Left = 177
      Top = 19
      Width = 15
      Height = 21
      Associate = Edit4
      Min = 3
      Max = 17
      Position = 10
      TabOrder = 6
    end
  end
  object BitBtn1: TBitBtn
    Left = 64
    Top = 240
    Width = 75
    Height = 25
    TabOrder = 3
    OnClick = BitBtn1Click
    Kind = bkOK
  end
  object BitBtn2: TBitBtn
    Left = 160
    Top = 240
    Width = 75
    Height = 25
    TabOrder = 4
    Kind = bkCancel
  end
end
