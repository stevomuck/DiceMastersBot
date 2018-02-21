﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Drawing;

namespace ConsoleApplication5.modules.Public
{
    public class imagebuilder : ModuleBase
    {
        GoogleTest go = new GoogleTest();
        static string url;

        [Command ("myteam")]
        public async Task combiner(string text)
        {
            string[] promonames = new string[]
            {
                "AVXOP", "BFFOP", "BFFPR", "D2016", "D2017", "JLOP", "M2015", "M2016", "M2017", "UXMOP", "UXMOP2", "WKO16D", "WKO16M"
            };

            string[] promoset = new string[10];

            string teamname = text.Substring(text.IndexOf("&name=") + 6);
            teamname = teamname.Replace("%20", " ");
            teamname = teamname.Replace("%26", "&");
            teamname = teamname.Replace("%27", "'");

            text = text.Substring(text.IndexOf("cards=") + 6);
            if(text.Contains("&name="))
            {
                text = text.Substring(0, text.IndexOf("&name="));
            }
            else
            {
                teamname = "Unnamed";
            }
            

            text = text.ToLower();
            List<string> list = text.Split(';').ToList(); //Creates a list

            string holder1;
            string holder2;
            int number;

            if(list.Count < 10)
            {
                int toappend = 10 - list.Count;

                for (int i = 0; i < toappend; i++)
                {
                    list.Add("1xblank1");
                }
            }

            await Context.Channel.SendMessageAsync("Compiling images");

            List<string> list2 = new List<string>();

            for (int i = 0; i < list.Count; i++) //Turn the list into readable indexes
            {
                bool contains = false;
                list2.Add(list[i].Substring(0, list[i].IndexOf("x")));
                
                list[i] = list[i].Substring(list[i].IndexOf("x") + 1);
                holder1 = Regex.Match(list[i], @"\d+").Value;

                holder2 = Regex.Replace(list[i], @"^[\d-]*\s*", string.Empty);
                
                for(int k = 0; k < promonames.Length; k++)
                {
                    if (holder2.ToUpper().Contains(promonames[k]))
                    {
                        contains = true;
                    }
                }
                
                number = Convert.ToInt32(holder1);

                if(number < 10)
                {
                    holder1 = "00" + holder1;
                }
                else if ((number >= 10) && (number < 100))
                {
                    holder1 = "0" + holder1;
                }

                //holder2 = new String(list[i].Where(Char.IsLetter).ToArray());

                if(holder2 == "imw")
                {
                    holder2 = "Imw";
                }
                else if(holder2 == "smc")
                {
                    holder2 = "smc";
                }
                else if (holder2 == "sww")
                {
                    holder2 = "sww";
                }
                else if (contains == true)
                {
                    promoset[i] = holder2;
                    holder2 = "PROMO";
                }
                //else if ((holder2 == "dc") || (holder2 == "m") || (holder2 == "bffop"))
                //{
                //    holder1 = "001";
                //    holder2 = "blank";
                //}

                if(contains != true)
                {
                    list[i] = holder2 + holder1;
                }
                else
                {
                    list[i] = holder2 + "#" + holder1;
                }

            }

            string sheet;
            string card;

            using (WebClient wc = new WebClient())
            {
                for (int j = 0; j < list.Count; j++)
                {
                    sheet = new String(list[j].Where(Char.IsLetter).ToArray());

                    if(list[j].Contains("PROMO"))
                    {
                        list[j] = list[j].Replace("PROMO", promoset[j]);
                    }

                    card = list[j].ToUpper();

                    string sheetref = sheet + "!";
                    GoogleTest.Main(sheetref);
                    var test = GoogleTest._values;

                    foreach (var row in test)
                    {
                        //Check for named cell
                        if ((string)row[0] == card)
                        {
                            //Assign the cells to varibales
                            url = (string)row[9];
                        }
                    }

                    using (Stream s = wc.OpenRead(url))
                    {
                        using (Bitmap bmp = new Bitmap(s))
                        {
                            bmp.Save("images/test" + j + ".jpg");
                            bmp.Dispose();
                        }
                    }
                }
            }

            await Context.Channel.SendMessageAsync("Scaling images");
            for (int k = 0; k < 10; k++)
            {
                Resize_Picture("images/test" + k + ".jpg", "images/testmod" + k + ".jpg", 0, 800, 10);
            }
            
            System.Drawing.Image source1 = System.Drawing.Image.FromFile("images/testmod0.jpg");
            System.Drawing.Image source2 = System.Drawing.Image.FromFile("images/testmod1.jpg");
            System.Drawing.Image source3 = System.Drawing.Image.FromFile("images/testmod2.jpg");
            System.Drawing.Image source4 = System.Drawing.Image.FromFile("images/testmod3.jpg");
            System.Drawing.Image source5 = System.Drawing.Image.FromFile("images/testmod4.jpg");
            System.Drawing.Image source6 = System.Drawing.Image.FromFile("images/testmod5.jpg");
            System.Drawing.Image source7 = System.Drawing.Image.FromFile("images/testmod6.jpg");
            System.Drawing.Image source8 = System.Drawing.Image.FromFile("images/testmod7.jpg");
            System.Drawing.Image source9 = System.Drawing.Image.FromFile("images/testmod8.jpg");
            System.Drawing.Image source10 = System.Drawing.Image.FromFile("images/testmod9.jpg");

            System.Drawing.Image overlay1 = System.Drawing.Image.FromFile(ovelay(list2[0]));
            System.Drawing.Image overlay2 = System.Drawing.Image.FromFile(ovelay(list2[1]));
            System.Drawing.Image overlay3 = System.Drawing.Image.FromFile(ovelay(list2[2]));
            System.Drawing.Image overlay4 = System.Drawing.Image.FromFile(ovelay(list2[3]));
            System.Drawing.Image overlay5 = System.Drawing.Image.FromFile(ovelay(list2[4]));
            System.Drawing.Image overlay6 = System.Drawing.Image.FromFile(ovelay(list2[5]));
            System.Drawing.Image overlay7 = System.Drawing.Image.FromFile(ovelay(list2[6]));
            System.Drawing.Image overlay8 = System.Drawing.Image.FromFile(ovelay(list2[7]));
            System.Drawing.Image overlay9 = System.Drawing.Image.FromFile(ovelay(list2[8]));
            System.Drawing.Image overlay10 = System.Drawing.Image.FromFile(ovelay(list2[9]));

            int toplevel = source1.Width + source2.Width + source3.Width + source4.Width;
            int bottomlevel = source4.Width + source5.Width + source6.Width + source7.Width;

            int picked = 0;

            if(toplevel > bottomlevel)
            {
                picked = toplevel;
            }
            else if (toplevel < bottomlevel)
            {
                picked = bottomlevel;
            }
            else if (toplevel == bottomlevel)
            {
                picked = bottomlevel;
            }

            Bitmap bitmap = new Bitmap(picked,
                source1.Height + source2.Height + source3.Height);

            await Context.Channel.SendMessageAsync("Combining images");

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source1, 0, 0);
                g.DrawImage(overlay1, (source1.Width / 2) - (overlay1.Width / 2), (source1.Height/2) - (overlay1.Height / 2));

                g.DrawImage(source2, source1.Width, 0);
                g.DrawImage(overlay2, source1.Width + (source2.Width / 2) - (overlay2.Width / 2), (source2.Height / 2) - (overlay2.Height / 2));

                g.DrawImage(source3, source1.Width + source2.Width, 0);
                g.DrawImage(overlay3, source1.Width + source2.Width + (source3.Width / 2) - (overlay3.Width / 2), (source3.Height / 2) - (overlay3.Height / 2));

                g.DrawImage(source4, source1.Width + source2.Width + source3.Width, 0);
                g.DrawImage(overlay4, source1.Width + source2.Width + source3.Width + (source4.Width / 2) - (overlay4.Width / 2), (source4.Height / 2) - (overlay4.Height / 2));

                g.DrawImage(source5, 0, source1.Height);
                g.DrawImage(overlay5, (source5.Width / 2) - (overlay5.Width/2), source1.Height + (source5.Height/2) - (overlay5.Height/2));


                g.DrawImage(source6, source1.Width, source2.Height);
                g.DrawImage(overlay6, source1.Width + (source6.Width / 2) - (overlay6.Width / 2), source2.Height + (source6.Height / 2) - (overlay6.Height / 2));

                g.DrawImage(source7, source1.Width + source2.Width, source3.Height);
                g.DrawImage(overlay7, source1.Width + source2.Width + (source7.Width / 2) - (overlay7.Width / 2), source3.Height + (source7.Height / 2) - (overlay7.Height / 2));


                g.DrawImage(source8, source1.Width + source2.Width + source3.Width, source4.Height);
                g.DrawImage(overlay8, source1.Width + source2.Width + source3.Width + (source8.Width / 2) - (overlay8.Width / 2), source4.Height + (source8.Height / 2) - (overlay8.Height / 2));


                g.DrawImage(source9, source1.Width, source2.Height + source6.Height);
                g.DrawImage(overlay9, source1.Width + (source9.Width / 2) - (overlay9.Width / 2), source2.Height + source6.Height + (source9.Height / 2) - (overlay9.Height / 2));


                g.DrawImage(source10, source1.Width + source2.Width, source3.Height + source7.Height);
                g.DrawImage(overlay10, source1.Width + source2.Width + (source10.Width / 2) - (overlay10.Width / 2), source3.Height + source7.Height + (source10.Height / 2) - (overlay10.Height / 2));

                bitmap.Save("images/combine2.jpg");
            }

            await Context.Channel.SendMessageAsync("**" + teamname + "**");
            await Context.Channel.SendFileAsync("images/combine2.jpg");
        }

        public static void Resize_Picture(string Org, string Des, int FinalWidth, int FinalHeight, int ImageQuality)
        {
            System.Drawing.Bitmap NewBMP;
            System.Drawing.Graphics graphicTemp;
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Org);

            int iWidth;
            int iHeight;
            if ((FinalHeight == 0) && (FinalWidth != 0))
            {
                iWidth = FinalWidth;
                iHeight = (bmp.Size.Height * iWidth / bmp.Size.Width);
            }
            else if ((FinalHeight != 0) && (FinalWidth == 0))
            {
                iHeight = FinalHeight;
                iWidth = (bmp.Size.Width * iHeight / bmp.Size.Height);
            }
            else
            {
                iWidth = FinalWidth;
                iHeight = FinalHeight;
            }

            NewBMP = new System.Drawing.Bitmap(iWidth, iHeight);
            graphicTemp = System.Drawing.Graphics.FromImage(NewBMP);
            graphicTemp.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            graphicTemp.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphicTemp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphicTemp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphicTemp.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphicTemp.DrawImage(bmp, 0, 0, iWidth, iHeight);
            graphicTemp.Dispose();
            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
            System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ImageQuality);
            encoderParams.Param[0] = encoderParam;
            System.Drawing.Imaging.ImageCodecInfo[] arrayICI = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (int fwd = 0; fwd <= arrayICI.Length - 1; fwd++)
            {
                if (arrayICI[fwd].FormatDescription.Equals("JPEG"))
                {
                    NewBMP.Save(Des, arrayICI[fwd], encoderParams);
                }
            }

            NewBMP.Dispose();
            bmp.Dispose();
        }

        public static string ovelay(string amount)
        {
            if (amount == "1")
            {
                return "images/x1.png";
            }
            else if (amount == "2")
            {
                return "images/x2.png";
            }
            else if (amount == "3")
            {
                return "images/x3.png";
            }
            else if (amount == "4")
            {
                return "images/x4.png";
            }
            else if (amount == "5")
            {
                return "images/x5.png";
            }
            else if (amount == "6")
            {
                return "images/x6.png";
            }
            else
            {
                return "images/x6.png";
            }


        }

    }
}
