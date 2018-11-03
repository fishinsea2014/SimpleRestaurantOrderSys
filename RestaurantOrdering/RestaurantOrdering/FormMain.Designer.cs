namespace RestaurantOrdering
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuManagerInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMemberInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTableInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDishInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::RestaurantOrdering.Properties.Resources.menuBg;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuManagerInfo,
            this.MenuMemberInfo,
            this.MenuTableInfo,
            this.MenuDishInfo,
            this.MenuOrder,
            this.MenuQuit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(753, 74);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuManagerInfo
            // 
            this.MenuManagerInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuManagerInfo.Image = global::RestaurantOrdering.Properties.Resources.menuManager;
            this.MenuManagerInfo.Name = "MenuManagerInfo";
            this.MenuManagerInfo.Size = new System.Drawing.Size(76, 68);
            this.MenuManagerInfo.Text = "toolStripMenuItem1";
            this.MenuManagerInfo.Click += new System.EventHandler(this.MenuManagerInfo_Click);
            // 
            // MenuMemberInfo
            // 
            this.MenuMemberInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuMemberInfo.Image = global::RestaurantOrdering.Properties.Resources.menuMember;
            this.MenuMemberInfo.Name = "MenuMemberInfo";
            this.MenuMemberInfo.Size = new System.Drawing.Size(76, 68);
            this.MenuMemberInfo.Text = "toolStripMenuItem2";
            // 
            // MenuTableInfo
            // 
            this.MenuTableInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuTableInfo.Image = global::RestaurantOrdering.Properties.Resources.menuTable;
            this.MenuTableInfo.Name = "MenuTableInfo";
            this.MenuTableInfo.Size = new System.Drawing.Size(76, 68);
            this.MenuTableInfo.Text = "toolStripMenuItem3";
            // 
            // MenuDishInfo
            // 
            this.MenuDishInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuDishInfo.Image = global::RestaurantOrdering.Properties.Resources.menuDish;
            this.MenuDishInfo.Name = "MenuDishInfo";
            this.MenuDishInfo.Size = new System.Drawing.Size(76, 68);
            this.MenuDishInfo.Text = "toolStripMenuItem4";
            // 
            // MenuOrder
            // 
            this.MenuOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuOrder.Image = global::RestaurantOrdering.Properties.Resources.menuOrder;
            this.MenuOrder.Name = "MenuOrder";
            this.MenuOrder.Size = new System.Drawing.Size(76, 68);
            this.MenuOrder.Text = "toolStripMenuItem5";
            // 
            // MenuQuit
            // 
            this.MenuQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuQuit.Image = global::RestaurantOrdering.Properties.Resources.menuQuit;
            this.MenuQuit.Name = "MenuQuit";
            this.MenuQuit.Size = new System.Drawing.Size(76, 68);
            this.MenuQuit.Text = "toolStripMenuItem6";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 692);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ordering System";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuManagerInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuMemberInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuTableInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuDishInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuOrder;
        private System.Windows.Forms.ToolStripMenuItem MenuQuit;
    }
}