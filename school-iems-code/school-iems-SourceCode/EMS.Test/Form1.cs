using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMS.Test
{
    public  partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = long.MaxValue.ToString() + "\n" + long.MinValue.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test2();
        }
        public bool IsValidYear( string year)
        {
            //return Regex.Match(year, @"^ (19 | 20)[0 - 9][0 - 9]").Success;
            if (Regex.IsMatch(year, @"^(18|19|20|21)[0-9][0-9]"))
            {
                return true;
            }
            return false;

        }
        void Test2()
        {

            string year = textBox1.Text;

            MessageBox.Show(IsValidYear(year).ToString());
        }

        void MathTest()
        {
            var dd = 16.1;

            decimal dt = (decimal)dd;


            richTextBox1.Text = Math.Ceiling(dd).ToString();
            Math.Round(12.3, MidpointRounding.AwayFromZero);
            // richTextBox1.Text = dt.ToString(); Decimal.Round((decimal)dd, 2).ToString();
            //richTextBox1.Text += "\n" + HiResDateTime.UtcNowTicks.ToString();
            //test();
        }


        private void test()
        {
            List<long> timeList = new List<long>(5000000);

            var starttime = DateTime.Now;
            Parallel.For(0, 5000000,
                    index =>
                    {
                        var val = HiResDateTime.UtcNowTicks2(); //long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                        //if (timeList.Contains(val))
                        //{
                        //    MessageBox.Show("Same id exist");
                        //}

                        //timeList.Add(val);
                    });
            var endtime = DateTime.Now;
            var diff = (endtime - starttime).TotalSeconds;
            MessageBox.Show("finished:" + diff.ToString());
        }

       
    }

    public class HiResDateTime
    {

        //DateTime.Now.ToString("yyyyMMddHHmmssffff")
        /// <summary>
        /// http://stackoverflow.com/questions/5608980/how-to-ensure-a-timestamp-is-always-unique
        /// </summary>
        private static long lastTimeStamp = DateTime.UtcNow.Ticks;
        public static long UtcNowTicks
        {
            get
            {

                long original, newValue;
                do
                {
                    original = lastTimeStamp;
                    long now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange
                             (ref lastTimeStamp, newValue, original) != original);

                return newValue;
            }
        }
        private static long lastTimeStamp2 = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        public static long UtcNowTicks2()
        {

            long original, newValue;
            do
            {
                original = lastTimeStamp2;
                long now = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssffff"));

                newValue = Math.Max(now, original + 1);
                //if (newValue == original)
                //    Debug.WriteLine(newValue.ToString());

            } while (Interlocked.CompareExchange
                         (ref lastTimeStamp2, newValue, original) != original);

            return newValue;

        }
        /*
        9223372036854775807

        635798030788240704
        201510071705340932
        2015 10 07 17 44 58 2940
        yyyy MM dd HH mm ss ffff

        5000000
        14.3056564
        14.223009
        14.3325506
        14.145404399999999
        3.7893339*/
    }
}
