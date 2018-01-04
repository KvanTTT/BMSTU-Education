/*
 * Created by SharpDevelop.
 * User: ?????????????
 * Date: 09.03.2009
 * Time: 12:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MGLab5
{
	/// <summary>
	/// Description of ColorSelectorPanel.
	/// </summary>
	public class ColorSelectorPanel: FlowLayoutPanel 
	{
		private static Size UnselectedSize = new Size(17,17);
		private static Size SelectedSize = new Size(25,25);
		private static Padding UnselectedPadding = new Padding(7);
        private static Padding SelectedPadding = new Padding(3);
        private System.ComponentModel.IContainer components;
		
		private List<Panel> ColorPanels;
		
		private void MakeLookAsSelected(Panel panel){
			panel.Size = SelectedSize;
			panel.Margin = SelectedPadding;
		}
		
		private void MakeLookAsUnselected(Panel panel){
			panel.Size = UnselectedSize;
			panel.Margin = UnselectedPadding;
		}
		
		private void ClickHandler(Object sender, EventArgs e){
			SuspendLayout();
			
			MakeLookAsSelected((Panel)sender);
			selectedcolor = ((Panel)sender).BackColor;
			selectedpen = new Pen(selectedcolor);
			
			foreach(Panel p in ColorPanels)
				if (p != sender){
					MakeLookAsUnselected(p);
				}
			
			ResumeLayout();
			
			if (ColorChanged != null)
				ColorChanged.Invoke(this, null);
		}
		
		public EventHandler ColorChanged = null;
		
		private Color selectedcolor;
		public Color SelectedColor{
			get{ return this.selectedcolor;} 
		}
		private Pen selectedpen;
		public Pen SelectedPen{
			get{ return this.selectedpen;} 
		}
		
		private void InsertColorPanel(Color color){
			Panel panel = new Panel();
			MakeLookAsUnselected(panel);
			
			panel.BackColor = color;
			panel.BorderStyle = BorderStyle.FixedSingle;
			
			panel.MouseClick += ClickHandler;
			
			ColorPanels.Add(panel);
			this.Controls.Add(panel);
		}
		
		public ColorSelectorPanel()
		{
			ColorPanels = new List<Panel>();
			
			SuspendLayout();
			
			InsertColorPanel(Color.Black);
			InsertColorPanel(Color.Green);
			InsertColorPanel(Color.Red);
			InsertColorPanel(Color.Orange);
			InsertColorPanel(Color.Cyan);
			InsertColorPanel(Color.Blue);
			InsertColorPanel(Color.White);
			
			selectedcolor = Color.Black;
			selectedpen = new Pen(selectedcolor);
			
			MakeLookAsSelected(ColorPanels[0]);
			ResumeLayout();
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
	}
}
