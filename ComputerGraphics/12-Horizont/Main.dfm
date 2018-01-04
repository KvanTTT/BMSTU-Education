object MainForm: TMainForm
  Left = 312
  Top = 140
  BorderStyle = bsSingle
  Caption = #1040#1083#1075#1086#1088#1080#1090#1084' '#1089' '#1087#1083#1072#1074#1072#1102#1097#1080#1084' '#1075#1086#1088#1080#1079#1086#1085#1090#1086#1084
  ClientHeight = 428
  ClientWidth = 558
  Color = 3214652
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  KeyPreview = True
  Menu = MainMenu
  OldCreateOrder = False
  OnCreate = FormCreate
  OnKeyDown = FormKeyDown
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Image: TImage
    Left = 0
    Top = 0
    Width = 558
    Height = 428
    Align = alClient
    ExplicitHeight = 409
  end
  object MainMenu: TMainMenu
    Left = 448
    Top = 10
    object N1: TMenuItem
      Caption = #1055#1072#1088#1072#1084#1077#1090#1088#1099
      object mnuFunction: TMenuItem
        Caption = #1060#1091#1085#1082#1094#1080#1103
        ImageIndex = 2
        object mnuF1: TMenuItem
          Caption = 'Z = 1.5 * Sin(3 * X) * Sin (2 * Y) * Cos(X * Y)'
          Checked = True
          RadioItem = True
          OnClick = mnuF1Click
        end
        object mnuF2: TMenuItem
          Caption = 'Z = 2 * Cox(x) - Sin(x) * Cos(y)'
          RadioItem = True
          OnClick = mnuF2Click
        end
        object mnuF3: TMenuItem
          Caption = 'Z = Sin(X + Exp(Y)) - Cos(Y)*Sinh(X)'
          RadioItem = True
          OnClick = mnuF3Click
        end
      end
      object mnuGrid: TMenuItem
        Caption = #1057#1077#1090#1082#1072
        OnClick = mnuGridClick
      end
    end
  end
end
