using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Media;

namespace EasyTimer {
    public partial class Form1 : Form {
        static String snd = @"C:\Windows\Media\Alarm01.wav";
        static SoundPlayer wavePlayer;
        const int defaultinterval = 25;

        public Form1() {
            InitializeComponent();
            timer1.Enabled = false;
            wavePlayer = new SoundPlayer();
            setTitle();
        }

        private void toggle() {
            bool en = timer1.Enabled;
            if ( en ) {
                button1.Text = "Start(&S)";
            } else {
                button1.Text = "Stop(&S)";
                int msec = decimal.ToInt32( numericUpDown1.Value ) * 60 * 1000;
                timer1.Interval = msec;
                if ( !System.IO.File.Exists( snd ) ) {
                    snd = @"C:\Windows\Media\tada.wav";
                }
                wavePlayer.SoundLocation = snd;
                wavePlayer.LoadAsync();
            }
            timer1.Enabled ^= true;
        }

        private void button1_Click( object sender, EventArgs e ) {
            toggle();
        }

        private void timer1_Tick( object sender, EventArgs e ) {
            toggle();
            wavePlayer.Play();
        }

        private void textBox1_TextChanged( object sender, EventArgs e ) {
        }

        private void button2_Click( object sender, EventArgs e ) {
            OpenFileDialog fp = new OpenFileDialog();
            fp.InitialDirectory = @"C:\Windows\Media";
            fp.Filter = @"WAVファイル(*.wav)|*.wav";
            if ( fp.ShowDialog() == DialogResult.OK ) {
                if ( fp.FileName.Length > 0 ) {
                    snd = fp.FileName;
                    setTitle();
                }
            }
        }

        private void setTitle() {
            String name = Path.GetFileName( snd );
            this.Text = $"EasyTimer - {name}";
        }

        private void button3_Click( object sender, EventArgs e ) {
            Application.Exit();
        }
    }
}
