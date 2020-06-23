namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.upButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.label_Text = new System.Windows.Forms.Label();
            this.label_ElapsedTime = new System.Windows.Forms.Label();
            this.label_MoveCount = new System.Windows.Forms.Label();
            this.ElapsedTime_DataText = new System.Windows.Forms.Label();
            this.MoveCount_DataText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(552, 278);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(58, 52);
            this.upButton.TabIndex = 11;
            this.upButton.TabStop = false;
            this.upButton.Text = "UP";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            this.upButton.Enter += new System.EventHandler(this.upButton_Enter);
            this.upButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(488, 318);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(58, 52);
            this.leftButton.TabIndex = 12;
            this.leftButton.TabStop = false;
            this.leftButton.Text = "LEFT";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            this.leftButton.Enter += new System.EventHandler(this.leftButton_Enter);
            this.leftButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(616, 318);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(58, 52);
            this.rightButton.TabIndex = 11;
            this.rightButton.TabStop = false;
            this.rightButton.Text = "RIGHT";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            this.rightButton.Enter += new System.EventHandler(this.rightButton_Enter);
            this.rightButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(552, 353);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(58, 52);
            this.downButton.TabIndex = 10;
            this.downButton.TabStop = false;
            this.downButton.Text = "DOWN";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            this.downButton.Enter += new System.EventHandler(this.downButton_Enter);
            this.downButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            // 
            // label_Text
            // 
            this.label_Text.Location = new System.Drawing.Point(486, 36);
            this.label_Text.Name = "label_Text";
            this.label_Text.Size = new System.Drawing.Size(188, 67);
            this.label_Text.TabIndex = 5;
            this.label_Text.Text = "Infomation \nmove : a s d w \nundo : q (max:10)";
            // 
            // label_ElapsedTime
            // 
            this.label_ElapsedTime.Location = new System.Drawing.Point(486, 120);
            this.label_ElapsedTime.Name = "label_ElapsedTime";
            this.label_ElapsedTime.Size = new System.Drawing.Size(65, 26);
            this.label_ElapsedTime.TabIndex = 6;
            this.label_ElapsedTime.Text = "경과 시간 :";
            // 
            // label_MoveCount
            // 
            this.label_MoveCount.Location = new System.Drawing.Point(486, 180);
            this.label_MoveCount.Name = "label_MoveCount";
            this.label_MoveCount.Size = new System.Drawing.Size(65, 26);
            this.label_MoveCount.TabIndex = 7;
            this.label_MoveCount.Text = "이동 횟수 :";
            // 
            // ElapsedTime_DataText
            // 
            this.ElapsedTime_DataText.Location = new System.Drawing.Point(572, 120);
            this.ElapsedTime_DataText.Name = "ElapsedTime_DataText";
            this.ElapsedTime_DataText.Size = new System.Drawing.Size(147, 26);
            this.ElapsedTime_DataText.TabIndex = 8;
            this.ElapsedTime_DataText.Text = "00:00";
            // 
            // MoveCount_DataText
            // 
            this.MoveCount_DataText.Location = new System.Drawing.Point(572, 180);
            this.MoveCount_DataText.Name = "MoveCount_DataText";
            this.MoveCount_DataText.Size = new System.Drawing.Size(147, 26);
            this.MoveCount_DataText.TabIndex = 9;
            this.MoveCount_DataText.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(742, 421);
            this.Controls.Add(this.MoveCount_DataText);
            this.Controls.Add(this.ElapsedTime_DataText);
            this.Controls.Add(this.label_MoveCount);
            this.Controls.Add(this.label_ElapsedTime);
            this.Controls.Add(this.label_Text);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.upButton);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Sliding_Puzzle!";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Label label_Text;
        private System.Windows.Forms.Label label_ElapsedTime;
        private System.Windows.Forms.Label label_MoveCount;
        private System.Windows.Forms.Label ElapsedTime_DataText;
        private System.Windows.Forms.Label MoveCount_DataText;        
    }
}

