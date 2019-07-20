using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace MusicSyncLib.WinForms
{
	public static class GuiUtils
	{
		public static void EnableDoubleBuffering(DataGridView dgv)
		{
			typeof(DataGridView).InvokeMember(
				"DoubleBuffered",
				BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
				null, dgv, new object[] { true });
		}

		public static void UpdateItemsStyle(DataGridView dgv, DataGridViewCellStyle style)
		{
			for (int i = 0; i < dgv.Columns.Count; i++)
				for (int j = 0; j < dgv.Rows.Count; j++)
				{
					var cell = dgv[i, j];
					if (cell.Style != style)
						cell.Style = style;
				}
			//dgv.DefaultCellStyle = style;
			//dgv.Update();
		}

		public static void UpdateItemsStyle(DataGridViewRow row, DataGridViewCellStyle style)
		{
			for (int i = 0; i < row.Cells.Count; i++)
				if (row.Cells[i].Style != style)
					row.Cells[i].Style = style;
		}

		public static void UpdateItemsColor(DataGridViewRow row, Color color)
		{
			for (int i = 0; i < row.Cells.Count; i++)
				if (row.Cells[i].Style.BackColor != color)
					row.Cells[i].Style.BackColor = color;
		}

		public static void UpdateItemsFont(DataGridViewRow row, Font font)
		{
			for (int i = 0; i < row.Cells.Count; i++)
				//if (row.Cells[i].Style.Font != font)
					row.Cells[i].Style.Font = font;
		}

		public static void UpdateItemStyles(DataGridView dgv, int startRow, int startColumn, int endRow, int endColumn, DataGridViewCellStyle style)
		{
			for (int j = startColumn; j < endColumn; j++)
			{
				if (j >= 0 && j < dgv.Columns.Count)
					for (int i = startRow; i < endRow; i++)
					{
						if (i >= 0 && i < dgv.Rows.Count)
						{
							var cell = dgv[i, j];
							if (cell.Style != style)
								cell.Style = style;
						}
					}
			}
		}

		public static void EnumerateRows(DataGridView dataGridView)
		{
			if (dataGridView.Rows.Count > 1)
				foreach (DataGridViewRow row in dataGridView.Rows)
					row.HeaderCell.Value = row.Index.ToString();
		}

		public static void NavigateToItem(DataGridView dataGridView, int row, int column, DataGridViewCellStyle cellStyle = null)
		{
			int scrollRow = row - (dataGridView.DisplayedRowCount(true) - 1) / 2;
			int scrollColumn = column - (dataGridView.DisplayedColumnCount(true) - 1) / 2;
			if (scrollRow < 0)
				scrollRow = 0;
			else if (scrollRow >= dataGridView.Rows.Count)
				scrollRow = dataGridView.Rows.Count - 1;
			if (scrollColumn < 0)
				scrollColumn = 0;
			else if (scrollColumn >= dataGridView.Columns.Count)
				scrollColumn = dataGridView.Columns.Count - 1;
			if (column >= 0 && column < dataGridView.Columns.Count && row >= 0 && row < dataGridView.Rows.Count)
			{
				dataGridView.CurrentCell = dataGridView[column, row];
				if (cellStyle != null)
					dataGridView.CurrentCell.Style = cellStyle;
				dataGridView.FirstDisplayedScrollingRowIndex = scrollRow;
				dataGridView.FirstDisplayedScrollingColumnIndex = scrollColumn;
			}
		}

		public static void BindMatrix(DataGridView dataGridView, double[,] matrix, String doubleFormat = "0.####")
		{
			/*DataGridViewColumn[] columns = new DataGridViewColumn[matrix.GetLength(1)];
			for (int j = 0; j < matrix.GetLength(1); j++)
				columns[j] = new DataGridViewColumn();*/
			dataGridView.Visible = false;
			dataGridView.Columns.Clear();
			for (int j = 0; j < matrix.GetLength(1); j++)
				dataGridView.Columns.Add("column" + j, j.ToString());
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				string[] rowValues = new string[matrix.GetLength(1)];
				for (int j = 0; j < matrix.GetLength(1); j++)
					rowValues[j] = matrix[i, j].ToString(doubleFormat);
				dataGridView.Rows.Add(rowValues);
			}
			GuiUtils.EnumerateRows(dataGridView);
			dataGridView.Visible = true;
		}

		public static void BindMatrix(DataGridView dataGridView, double[] matrix)
		{
			dataGridView.Visible = false;
			dataGridView.Columns.Clear();
			string[] rowValues = new string[matrix.Length];
			for (int j = 0; j < matrix.Length; j++)
			{
				dataGridView.Columns.Add("column" + j, j.ToString());
				rowValues[j] = matrix[j].ToString("0.####");
			}
			dataGridView.Rows.Add(rowValues);
			GuiUtils.EnumerateRows(dataGridView);
			dataGridView.Visible = true;
		}

		public static void BindMatrix(DataGridView dataGridView, int[] matrix)
		{
			dataGridView.Columns.Clear();
			string[] rowValues = new string[matrix.Length];
			for (int j = 0; j < matrix.Length; j++)
			{
				dataGridView.Columns.Add("column" + j, j.ToString());
				rowValues[j] = matrix[j].ToString();
			}
			dataGridView.Rows.Add(rowValues);
			GuiUtils.EnumerateRows(dataGridView);
		}

		public static void AppendText(this RichTextBox box, string text, Color color)
		{
			box.SelectionStart = box.TextLength;
			box.SelectionLength = 0;
			box.SelectionColor = color;
			box.AppendText(text);
			box.SelectionColor = box.ForeColor;
		}
	}
}
