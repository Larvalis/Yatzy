using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YahtzeeLibrary;

namespace YahtzeeApp
{
    public partial class Form1 : Form
    {
        int RollNr=0;

        Yahtzee yahtzee = null;

        public Form1()
        {
            InitializeComponent();

            yahtzee = new Yahtzee();

        }

        // shrow dies
        private void btnRoll_Click(object sender, EventArgs e)
        {
            yahtzee.Roll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked);

            showDies();

            RollNr++;
            btnRoll.Text = "Roll (" + (RollNr + 1)+")";
            if (RollNr == 3)
            {
                RollNr = 0;
                clearHold();
                btnRoll.Visible = false;
                calculateScore();
            }
            btnRoll.Text = "Roll (" + (RollNr + 1)+")";
                        
            enableHold();
        }

        // show dies in GUI
        private void showDies()
        {
            textBox1.Text = "" + yahtzee.Dies[0];
            textBox2.Text = "" + yahtzee.Dies[1];
            textBox3.Text = "" + yahtzee.Dies[2];
            textBox4.Text = "" + yahtzee.Dies[3];
            textBox5.Text = "" + yahtzee.Dies[4];
        }

        // all TextBox'e with value of kombination must have this DoubleClick event added
        // the event fixes the value double clicked, disbale the control and clear all enabled
        // textBoxes
        private void tBoxValue_DoubleClick(object sender, EventArgs e)
        {
            (sender as TextBox).Enabled = false;
            (sender as TextBox).BackColor = Color.White;
            bool finished=clearAllYellow();
            clearAllDies();
            if (!finished) btnRoll.Visible = true;
            RollNr = 0;

            calculateSumAndBonus();

            if (finished)
                MessageBox.Show("The game is over!");
        }

        private void clearAllDies()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        // sets the value in a TextBox and sets BackColor to yellow
        private void setValue(TextBox tBox, int value)
        {
            tBox.Text = "" + value;
            tBox.BackColor = Color.Yellow;
        }

        // i txt=="" returns 0, else converts txt to int32 and returns value
        private int toInt32(string txt) {
            if (txt == "")
                return 0;

            return Convert.ToInt32(txt);
        }

        // skal beregner summer og bonus
        // MANGLER
        // tag værdierne fra vinduet og beregn summer og check om der er bonus
        // der er bonus på 50 piont såfremt summer af 6 øverste kombinationer er mindst 63
        // bergen også point ialt
        private void calculateSumAndBonus()
        {
            int sum1 = 0;
            sum1 += toInt32(tBox1ere.Text);
            sum1 += toInt32(tBox2ere.Text);
            sum1 += toInt32(tBox3ere.Text);
            sum1 += toInt32(tBox4ere.Text);
            sum1 += toInt32(tBox5ere.Text);
            sum1 += toInt32(tBox6ere.Text);

            tBoxSumUpper.Text = "" + sum1;

            int bonus = 0;
            if (sum1 >= 63) bonus = 35;
            tBoxBonus.Text = "" + bonus;

            int sum2 = 0;
            sum2+=toInt32(tBoxEtPar.Text);
            sum2 += toInt32(tBoxToPar.Text);
            sum2 += toInt32(tBoxTreEns.Text);
            sum2 += toInt32(tBoxFireEns.Text);

            // lille, stor, fuldt hus, yatzy
            sum2 += toInt32(tBoxLilleStraight.Text);
            sum2 += toInt32(tBoxStorStraight.Text);
            sum2 += toInt32(tBoxFuldtHus.Text);
            sum2 += toInt32(tBoxYatzy.Text);
            sum2 += toInt32(tBoxChance.Text);

            tBoxSumLower.Text = "" + sum2;

            tBoxTotal.Text=""+(sum1+bonus+sum2);
        }

        // beregner point i alle ikke benyttede kombinationer
        private void calculateScore()
        {
            // ens
            if (tBox1ere.Enabled) setValue(tBox1ere, yahtzee.valueSpecificFace(1));
            if (tBox2ere.Enabled) setValue(tBox2ere, yahtzee.valueSpecificFace(2));
            if (tBox3ere.Enabled) setValue(tBox3ere, yahtzee.valueSpecificFace(3));
            if (tBox4ere.Enabled) setValue(tBox4ere, yahtzee.valueSpecificFace(4));
            if (tBox5ere.Enabled) setValue(tBox5ere, yahtzee.valueSpecificFace(5));
            if (tBox6ere.Enabled) setValue(tBox6ere, yahtzee.valueSpecificFace(6));

            // et par, to par, tre ens fire ens
            if (tBoxEtPar.Enabled) setValue(tBoxEtPar, yahtzee.valueOnePair());
            if (tBoxToPar.Enabled) setValue(tBoxToPar, yahtzee.valueTwoPair());
            if (tBoxTreEns.Enabled) setValue(tBoxTreEns, yahtzee.valueSameOfAKind(3));
            if (tBoxFireEns.Enabled) setValue(tBoxFireEns, yahtzee.valueSameOfAKind(4));

            // lille, stor, fuldt hus, yatzy
            if (tBoxLilleStraight.Enabled) setValue(tBoxLilleStraight, yahtzee.valueSmallStraight());
            if (tBoxStorStraight.Enabled) setValue(tBoxStorStraight, yahtzee.valueLargeStraight());
            if (tBoxFuldtHus.Enabled) setValue(tBoxFuldtHus, yahtzee.valueFullHouse());
            if (tBoxYatzy.Enabled) setValue(tBoxYatzy, yahtzee.valueYatzy());
            if (tBoxChance.Enabled) setValue(tBoxChance, yahtzee.valueChance());
        }


        // sæt HoldEnabled
        private void enableHold()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
        }

        // reset Hold checkbox'e
        private void clearHold()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
        }

        // clear alle gule TextBox'e
        // returns wether the game is over
        private bool clearAllYellow()
        {
            int n = 0;
            foreach (Control ctrl in groupBox1.Controls)
                if (ctrl is TextBox && ctrl.BackColor == Color.Yellow)
                {
                    n++;
                    ctrl.BackColor = Color.White;
                    ctrl.Text = "";
                }

            return n == 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
                if (control is TextBox)
                {
                    (control as TextBox).ReadOnly = true;
                }
        }

    }
}
