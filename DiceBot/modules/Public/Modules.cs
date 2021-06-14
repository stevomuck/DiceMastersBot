using System;
using System.Collections;
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

namespace ConsoleApplication5.modules.Public
{
    public class Modules : ModuleBase<SocketCommandContext>
    {
        int sheets;
        string number;
        string Tempname = "";
        string Tempnick = "";
        string Tempset = "";
        string Tempsub = "";
        static string CName = "";
        string CSub = "";
        string CCost = "";
        string CEnergy = "";
        string CRarity = "";
        string CAffiliation = "";
        string CEffect1 = "";
        string CEffect = "";
        string CStat = "";
        string CImage = "";
        string CFImage = "";
        string CCode = "";
        string CGlobal = "";
        string holder = "";
        bool global = false;

        List<string> valid = new List<string>(); 
        Color rarity = new Color(0, 255, 0);
        GoogleTest go = new GoogleTest();
 

        static List<string> setlines = System.IO.File.ReadLines(@"TextFiles/Sets.txt").ToList();

        static List<string> setnamelines = System.IO.File.ReadLines(@"TextFiles/Setname.txt").ToList();

        static List<string> affiliationlines = System.IO.File.ReadLines(@"TextFiles/Affiliations.txt").ToList();

        static List<string> AffiliationEmojilines = System.IO.File.ReadLines(@"TextFiles/AffilliationEmoji.txt").ToList();

        static List<string> keywordlines = System.IO.File.ReadLines(@"TextFiles/KeywordList.txt").ToList();


        static string[] Rarities = new string[]
        {
            "c", "ch", "uc", "u", "r", "sr", "s", "p", "common", "uncommon", "rare", "super", "super rare", "chase", "promo"
        };

        [Command("dmad")]
        public async Task eggplant()
        {
            string michaelaid = "329677709808500746";
            string me = "189448403355172864";
            string id = Context.User.Id.ToString();
            if ((id == michaelaid) || (id == me))
            {
                await Context.Channel.SendMessageAsync("*sigh* ... :eggplant:");
            }
            else
            {
                await Context.Channel.SendMessageAsync("This command is reserved for... you know who...");
            }
        }

        [Command("addset")]
        public async Task update(string setcode, string setfullname)
        {
            //File.AppendAllText(@"Sets.txt", Environment.NewLine + setcode);

            using (StreamWriter sw = File.AppendText(@"TextFiles/Sets.txt"))
            {
                sw.WriteLine(setcode.ToUpper());
                sw.Close();
            }
            string[] setarray = File.ReadAllLines(@"TextFiles/Sets.txt");
            File.WriteAllLines(@"TextFiles/Sets.txt", setarray);

            using (StreamWriter sw2 = File.AppendText(@"TextFiles/Setname.txt"))
            {
                sw2.WriteLine(setfullname);
                sw2.Close();
            }
            string[] setnamearray = File.ReadAllLines(@"TextFiles/Setname.txt");
            File.WriteAllLines(@"TextFiles/Setname.txt", setnamearray);

            setlines = System.IO.File.ReadLines(@"TextFiles/Sets.txt").ToList();

            //File.AppendAllText(@"Setname.txt", Environment.NewLine + setfullname);
            setnamelines = System.IO.File.ReadLines(@"TextFiles/Setname.txt").ToList();

            await Context.User.SendMessageAsync("I have updated");
        }

        [Command("addafil")]
        public async Task Affiliateupdate(string setcode, string setfullname)
        {
            using (StreamWriter sw = File.AppendText(@"TextFiles/Affiliations.txt"))
            {
                sw.WriteLine(setcode.ToUpper());
                sw.Close();
            }
            string[] setarray = File.ReadAllLines(@"TextFiles/Affiliations.txt");
            File.WriteAllLines(@"TextFiles/Affiliations.txt", setarray);
            await Context.User.SendMessageAsync("I have updated");

        }

        [Command("addafilemo")]
        public async Task AffiliateEmojiupdate(string setcode, string setfullname)
        {
            using (StreamWriter sw = File.AppendText(@"TextFiles/AffiliationEmoji.txt"))
            {
                sw.WriteLine(setcode.ToUpper());
                sw.Close();
            }
            string[] setarray = File.ReadAllLines(@"TextFiles/AffiliationEmoji.txt");
            File.WriteAllLines(@"TextFiles/AffiliationEmoji.txt", setarray);
            await Context.User.SendMessageAsync("I have updated");

        }

        [Command("addkey")]
        public async Task keywordupdate(string setcode, string setfullname)
        {
            using (StreamWriter sw = File.AppendText(@"TextFiles/KeywordList.txt"))
            {
                sw.WriteLine(setcode.ToUpper());
                sw.Close();
            }
            string[] setarray = File.ReadAllLines(@"TextFiles/KeywordList.txt");
            File.WriteAllLines(@"TextFiles/KeywordList.txt", setarray);
            await Context.User.SendMessageAsync("I have updated");

        }

        [Command("sets")]
        public async Task setnames()
        {
            string[] sets = setlines.ToArray();
            string[] setname = setnamelines.ToArray();

            string s = "Sets available in the bot :  ```";
            int check = setname.Length;

            for (int i = 0; i < sets.Length; i++)
            {
                s += String.Format("{0, -4 }", sets[i]);
                s += " | ";
                s += String.Format("{0,-30}", setname[i]);
                s += "\n";
            }

            s += "```";
            await Context.User.SendMessageAsync(s);
        }

        [Command("setshere")]
        public async Task setnameschannel()
        {
            string[] sets = setlines.ToArray();
            string[] setname = setnamelines.ToArray();

            string s = "Sets available in the bot :  ```";

            for (int i = 0; i < sets.Length; i++)
            {
                s += String.Format("{0, -4 }", sets[i]);
                s += " | ";
                s += String.Format("{0,-30}", setname[i]);
                s += "\n";

            }

            s += "```";
            await Context.Channel.SendMessageAsync(s);
        }

        [Command("ping")]
        public async Task ping()
        {
            await Context.Channel.SendMessageAsync("```prolog" + "\n#test \ntest" + "```");
        }

        [Command("help")]
        public async Task DM()
        {
            
            string s = "Okay, so here is a list of my functions: \n\n" +
                ">card \n" +
                ">global \n" +
                ">nick \n" +
                ">keyword \n" +
                ">myteam \n" +
                ">myteamgif \n" +
                ">promo \n" +
                ">fullpic \n" +
                ">fulltext \n" +
                ">sets \n" +
                ">setshere \n" +
                ">super \n" +
                ">about \n\n\nIf you want to see an example please type >help and then the function name for a more detailed explanation.";
            await ReplyAsync(s);

        }

        [Command("help")]
        public async Task DMHelpDetails(string function)
        {
            string s;

            if ((function == ">card") || (function == "card"))
            {
                s = "There are several versions of this command. Here is how to use them. We will look at Rip Hunter from the BAT set for demonstration" + "```" +
                 ">card (cardset and number combined)\n \t\t Example: >card BAT030 <= This returns Rip Hunter: Navigating the Sands of Time.\n\n" +
                 ">card (cardset) (card name)\n \t\t Example: >card BAT \"Rip Hunter\" <= This returns all Rip Hunter cards in the set.\n\n" +
                 ">card (cardset) (card name) (rarity)\n \t\t Example: >card BAT \"Rip Hunter\" R <= This returns the Rare Rip Hunter.```" +
                 "\n\nPlease be aware that if the card name has more than 1 word, it will need to be encased in \"\". Example \"The Bifrost\" ";
            }
            else if ((function == ">global") || (function == "global"))
            {
                s = "To use this function you just need to include the set and the character/card name. Here is a demonstration using THOR and Samantha Wilson" + "```" +
                    ">global (card name)\n \t\t>global \"Samantha Wilson\" <= This will return all the global abilites found on all the rarites. If there are 2 or more different ones then they will be printed seperately.```" +
                 "\n\nPlease be aware that if the card name has more than 1 word, it will need to be encased in \"\". Example \"The Bifrost\" ";
            }
            else if ((function == ">nick") || (function == "nick"))
            {
                s = "There are several versions of this command. Here is how to use them. We will look at Lantern Ring from the WOL set for demonstration, which is called \"Ring\" for short. (As in a Bolt Ring team)" + "```" +
                ">card (nickname)\n \t\t Example: >nick Ring <= This returns the Rare Lantern Ring.\n\n" +
                ">card (cardset) (card name)\n \t\t Example: >card Wol Ring <= This returns Rare Lantern Ring.```";
            }
            else if ((function == ">promo") || (function == "promo"))
            {
                s = "There are several versions of this command. Here is how to use them. For this demonstration we will look at Thor: The Mighty from AVX" + "```" +
                ">card (cardset and number combined)\n \t\t Example: >promo AVXOP#007 <= This returns the promo Thor card.\n\n" +
                ">card (card name)\n \t\t Example: >promo Thor <= This returns all Thor promos.```" +
                 "\n\nPlease be aware that if the card name has more than 1 word, it will need to be encased in \"\". Example \"Wonder Woman\" ";
            }
            else if ((function == ">keyword") || (function == "keyword"))
            {
                s = "To use this function, you need only state the keyword you are after. For this demonstration we will look at Breath Weapon" + "```" +
                ">keyword (keyword you are after)\n \t\t Example: >keyword \"Breath Weapon\" <= This returns the text from the wizkids keywords page, in regards to the ruling.```\n\n" +
                 "\n\nPlease be aware that if the card name has more than 1 word, it will need to be encased in \"\". Example \"Call Out\" \n\n" +
                 "This command is known to be broken and may not work, it is being worked on.";
            }
            else if ((function == ">myteam") || (function == "myteam"))
            {
                s = "To use this function, you just need to paste a team link from one of these 3 sites. http://tb.dicecoalition.com/, http://dm.frankenstein.com/ or http://dm.retrobox.eu/ " + "```" +
                ">myteam (url)\n \t\t Example: >myteam http://tb.dicecoalition.com/?view&cards=1x16smc <= I will build a team image with these cards, inserting blanks if needed.```";
            }
            else if ((function == ">myteam") || (function == "myteam"))
            {
                s = "To use this function, you just need to state if you want to have dice quantities printed and paste a team link from one of these 3 sites. http://tb.dicecoalition.com/, http://dm.frankenstein.com/ or http://dm.retrobox.eu/ " + "```" +
                ">myteam (url)\n \t\t Example: >myteam dice http://tb.dicecoalition.com/?view&cards=1x16smc <= I will build a team gif with these cards, printing the dice quantities on them and inserting blanks if needed." +
                "\nExample: >myteam nodice http://tb.dicecoalition.com/?view&cards=1x16smc <= I will build a team gif with these cards, inserting blanks if needed.```";
            }
            else if ((function == ">fullpic") || (function == "fullpic"))
            {
                s = "This function will pm you all the cards in a set, with their images if available." + "```" +
                ">fullpic (set)\n \t\t Example: >fullpic THOR <= This sends all the THOR cards to you in private to avoid spam.```";
            }
            else if ((function == ">fulltext") || (function == "fulltext"))
            {
                s = "This function will pm you all the cards in a set, without their images." + "```" +
                ">fullpic (set)\n \t\t Example: >fullpic THOR <= This sends all the THOR cards to you in private to avoid spam.```";
            }
            else if ((function == ">sets") || (function == "sets"))
            {
                s = "This function will pm you all the sets available in the bot." + "```" +
                "Example: >sets <= This sends all the sets and their abbreviations to you in private.```";
            }
            else if ((function == ">setshere") || (function == "setshere"))
            {
                s = "This function will print all the sets available in the bot in the active channel." + "```" +
                "Example: >setshere <= This prints all the sets and their abbreviations in the active channel.```";
            }
            else if ((function == ">super") || (function == "super"))
            {
                s = "This function will print out all the super rare cards in a set, with their images if available." + "```" +
                ">super (set)\n \t\t Example: >super THOR <= This prints all the Super Rare THOR cards to the active channe.```";
            }
            else if ((function == ">about") || (function == "about"))
            {
                s = "To use this command, just type >about. Its pretty simple!";
            }
            else
            {
                s = "I cant find that function!";
            }

            await ReplyAsync(s);
        }

        [Command("about")]
        public async Task About()
        {
            string s = "This bot was made in C# using the .Net framework and api's from Google and Discord. The programmer is Stevomuck, who can be found on the server or on facebook as Steven McEwan." +
                "\nIf you find any errors or have a function idea, please let him know! (also if you want to tip him, he will be more than happy to send you paypal information!)";
            await ReplyAsync(s);
        }


        [Command("card"), Summary("Echos a message.")]
        [Alias("carte")]
        public async Task say([Remainder, Summary("The text to echo")] string echo)
        {
            string[] sets = setlines.ToArray();

            echo = echo.Replace('“', '"');
            echo = echo.Replace('”', '"');

            var parameter = Regex.Matches(echo, @"[\""].+?[\""]|[^ ]+")
                            .Cast<Match>()
                            .Select(m => m.Value)
                            .ToList();

            for (int i = 0; i < parameter.Count(); i++)
            {
                if(parameter[i].Contains("\""))
                {
                    parameter[i] = parameter[i].Replace("\"", "");
                }
            }

            if(parameter.Count == 3)
            {
                await FindCard(parameter[0], parameter[1], parameter[2]);
            }
            else if (parameter.Count == 1)
            {
                await DisplayCards(parameter[0]);
            }
            else if (parameter.Count == 2)
            {
                await FindCard(parameter[0], parameter[1]);
            }

        }

        public async Task DisplayCards(string value)
        {
            string[] sets = setlines.ToArray();
            bool found = false;
            string card = value;

            value = value.ToUpper();

            string[] promonames = new string[]
            {
                "AVXOP", "BFFOP", "BFFPR", "D2016","DC2017", "D2017", "JLOP", "M2015", "M2016", "M2017", "UXMOP", "UXMOP2", "WKO16D", "WKO16M"
            };

            bool contains = false;

            for(int i = 0; i < promonames.Length; i++)
            {
                if (value.Contains(promonames[i]))
                {
                    contains = true;
                }
            }

            string sheet = "";
            if(contains != true)
            {
                sheet = Regex.Replace(value, @"[\d-]", string.Empty);
            }
            else
            {
                sheet = "PROMO";
            }
            


            card = card.ToUpper();
            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
                found = true;
            }
            else
            {

                if (card == "random")
                {
                    Random rnd = new Random();
                    sheets = rnd.Next(0, sets.Length);

                    sheet = await randomSheet(sheets);
                    number = randomNumber(sheet);

                    card = sheet.ToUpper() + number;
                }

                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                if(contains == true)
                {
                    if (!Char.IsLetter(card.FirstOrDefault()))
                    {
                        
                        sheet = Regex.Replace(value, @"^[\d-]*\s*", string.Empty);
                        string cardtemp = card.Replace(sheet, "");
                        int digits = cardtemp.Count();
                        for (int j = digits; j < 3; j++)
                        {
                            cardtemp = "0" + cardtemp;
                        }

                        card = sheet + "#" + cardtemp;

                    }
                    else
                    {
                        if (card.Contains("#"))
                        {
                            string cardno = card.Substring(card.IndexOf('#') + 1);
                            string cardset = card.Replace(cardno, "");
                            int digits = cardno.Count();
                            for (int j = digits; j < 3; j++)
                            {
                                cardno = "0" + cardno;
                            }

                            card = cardset + cardno;
                        }
                        else
                        {
                            string code = card.Substring(card.Length - 3);
                            string set = card.Replace(code, "");
                            card = set + "#" + code;
                        }
                    }
                }
                else
                {
                    
                    string cardtemp = card.Replace(sheet, "");
                    int digits = cardtemp.Count();
                    for (int j = digits; j < 3; j++)
                    {
                        cardtemp = "0" + cardtemp;
                    }

                    card = sheet + cardtemp;
                }



                foreach (var row in test)
                {
                    //Check for named cell
                    if ((string)row[0] == card)
                    {
                        
                        //Assign the cells to varibales
                        CCode = (string)row[0];
                        CName = (string)row[1];
                        CSub = (string)row[2];
                        CCost = (string)row[3];
                        CEnergy = (string)row[4];
                        CRarity = (string)row[5];
                        CAffiliation = (string)row[6];
                        CEffect1 = (string)row[7];
                        CEffect = CEffect1;
                        CStat = (string)row[8];
                        CImage = (string)row[9];
                        CFImage = (string)row[10];
                        found = true;
                    }
                }

                if (CEnergy == "x")
                {
                    CEnergy = " ";
                }

                if (CAffiliation == "x")
                {
                    CAffiliation = " ";
                }
                await contextbuilder(2);
            }

            if (found == false)
            {
                await Context.Channel.SendFileAsync("hall.png");
                await Context.Channel.SendMessageAsync("Oh dear," + Context.User.Mention + "\n\n I don't appear to have that one recoreded.");
            }
        }

        public async Task FindCard(string sheet, string name)
        {
            string[] sets = setlines.ToArray();
            bool found = false;
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";


            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
                found = true;
            }
            else
            {
                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                name = name.ToLower();

                foreach (var row in test)
                {
                    Tempname = (string)row[1];
                    Tempname = Tempname.ToLower();

                    holder = (string)row[1];
                    if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                    {
                        if (holder.Contains(' '))
                        {
                            holder = "\"" + holder + "\"";

                        }
                        valid.Add(holder);
                    }

                    //Check for named cell
                    if (Tempname == name)
                    {
               
                        //Assign the cells to varibales
                        CCode = (string)row[0];
                        CName = (string)row[1];
                        CSub = (string)row[2];
                        CCost = (string)row[3];
                        CEnergy = (string)row[4];
                        CRarity = (string)row[5];
                        CAffiliation = (string)row[6];
                        CEffect = (string)row[7];
                        CStat = (string)row[8];
                        CImage = (string)row[9];
                        CFImage = (string)row[10];

                        found = true;

                        if ((CSub != "Basic Action Card") || (CRarity != "Promo"))
                        {
                            await contextbuilder(1);
                        }
                        else
                        {
                            await contextbuilder(2);
                        }
                    }
                }
            }
            if (found == false)
            {
                await Error("card", name);
            }
        }

        public async Task FindCard(string sheet, string name, string rarity)
        {
            string[] sets = setlines.ToArray();
            bool found = false;
            bool subtitlemode = false;
            string subtitle = "";
            string finalrarity = "";
            bool errorcheck = false;
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";

            rarity = rarity.ToLower();

            if ((rarity == "c") || (rarity == "common"))
            {
                finalrarity = "Common";
            }
            else if ((rarity == "uc") || (rarity == "u") || (rarity == "uncommon"))
            {
                finalrarity = "Uncommon";
            }
            else if ((rarity == "r") || (rarity == "rare"))
            {
                finalrarity = "Rare";
            }
            else if ((rarity == "sr") || (rarity == "s") || (rarity == "super") || (rarity == "super rare"))
            {
                finalrarity = "Super";
            }
            else if ((rarity == "p") || (rarity == "promo"))
            {
                finalrarity = "Promo";
            }
            else if ((rarity == "ch") || (rarity == "chase"))
            {
                finalrarity = "Chase";
            }
            else
            {
                subtitlemode = true;
                subtitle = rarity;
            }

            if (!sets.Contains(sheet.ToUpper()))
            {
                errorcheck = true;
            }

            if ((!Rarities.Contains(rarity.ToLower())) && (subtitlemode == false))
            {
                errorcheck = true;
            }

            if (errorcheck == true)
            {
                if (!sets.Contains(sheet.ToUpper()))
                {
                    await Error("set", sheet);
                    found = true;
                }
                if (subtitlemode == false)
                {
                    if (!Rarities.Contains(rarity.ToLower()))
                    {
                        await Error("rarity", "blank");
                        found = true;
                    }
                }
            }
            else
            {
                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;



                name = name.ToLower();
                int count = 0;



                foreach (var row in test)
                {
                    Tempname = (string)row[1];
                    Tempname = Tempname.ToLower();
                    Tempsub = (string)row[2];
                    Tempsub = Tempsub.ToLower();
                    //Check for named cell

                    if ((Tempname == name) && (subtitlemode == false ? ((string)row[5] == finalrarity) : (Tempsub == subtitle)))
                    {
                        count++;
                    }
                }

                //Build the array of characters for the error module.
                foreach (var row in test)
                {
                    Tempname = (string)row[1];
                    Tempname = Tempname.ToLower();
                    Tempsub = (string)row[2];
                    Tempsub = Tempsub.ToLower();

                    holder = (string)row[1];
                    if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                    {
                        if (holder.Contains(' '))
                        {
                            holder = "\"" + holder + "\"";

                        }
                        valid.Add(holder);
                    }

                    //Check for named cell
                    if ((Tempname == name) && (subtitlemode == false ? ((string)row[5] == finalrarity) : (Tempsub == subtitle)))
                    {
                        //Assign the cells to varibales
                        CCode = (string)row[0];
                        CName = (string)row[1];
                        CSub = (string)row[2];
                        CCost = (string)row[3];
                        CEnergy = (string)row[4];
                        CRarity = (string)row[5];
                        CAffiliation = (string)row[6];
                        CEffect = (string)row[7];
                        CStat = (string)row[8];
                        CImage = (string)row[9];
                        CFImage = (string)row[10];
                        found = true;
                        if (count > 1)
                        {
                            await contextbuilder(1);
                        }
                        else
                        {
                            await contextbuilder(2);
                        }
                    }
                }

            }
            if (found == false)
            {
                await Error("card", name);
            }

        }

        [Command ("global")]
        public async Task FindGlobals(string sheet, string name)
        {
            string[] sets = setlines.ToArray();
            List<string> effectholder = new List<string>();

            bool foundglobal = false;
            bool foundcharacter = false;
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";


            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
                foundglobal = true;
            }
            else
            {
                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                name = name.ToLower();

                foreach (var row in test)
                {
                    Tempname = (string)row[1];
                    Tempname = Tempname.ToLower();

                    holder = (string)row[1];
                    if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                    {
                        if (holder.Contains(' '))
                        {
                            holder = "\"" + holder + "\"";

                        }
                        valid.Add(holder);
                    }

                    

                    //Check for named cell
                    if (Tempname == name)
                    {
                        foundcharacter = true;

                        string fullEffect = (string)row[7];
                        string find = "Global: ";

                        if (fullEffect.Contains("Global: "))
                        {
                            foundglobal = true;
                            string global = fullEffect.Substring(fullEffect.IndexOf(find) + find.Length);

                            if (!effectholder.Contains(global))
                            {
                                effectholder.Add(global);
                                CGlobal = global;
                                await globalprint(name);
                            }
                        }
                    }
                }
            }
            if (foundglobal == false)
            {
                if (foundcharacter == false)
                {
                    await Error("card", name);
                }
                else
                {
                    await Error("global", name);
                }
            }
        }
    
        [Command("nick")]
        public async Task FindNick(string sheet, string nick)
        {
            string[] sets = setlines.ToArray();
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";

            bool found = false;
            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
                found = true;
            }
            else
            {
                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                nick = nick.ToLower();

                foreach (var row in test)
                {
                    Tempnick = (string)row[11];
                    Tempnick = Tempnick.ToLower();

                    holder = (string)row[1];
                    if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                    {
                        if (holder.Contains(' '))
                        {
                            holder = "\"" + holder + "\"";

                        }
                        valid.Add(holder);
                    }

                    //Check for named cell
                    if (Tempnick == nick)
                    {
                        //Assign the cells to varibales
                        CCode = (string)row[0];
                        CName = (string)row[1];
                        CSub = (string)row[2];
                        CCost = (string)row[3];
                        CEnergy = (string)row[4];
                        CRarity = (string)row[5];
                        CAffiliation = (string)row[6];
                        CEffect = (string)row[7];
                        CStat = (string)row[8];
                        CImage = (string)row[9];
                        CFImage = (string)row[10];
                        found = true;
                        await contextbuilder(2);
                    }
                }
            }
            if (found == false)
            {
                await Error("card","blank");
            }
        }

        [Command("nick")]
        public async Task FindNick(string nick)
        {
            bool found = false;
            string sheetref = "NICKNAMES!";
            GoogleTest.Main(sheetref);
            var test = GoogleTest._values;

            nick = nick.ToLower();

            foreach (var row in test)
            {
                Tempnick = (string)row[11];
                Tempnick = Tempnick.ToLower();

                holder = (string)row[1];
                if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                {
                    if (holder.Contains(' '))
                    {
                        holder = "\"" + holder + "\"";

                    }
                    valid.Add(holder);
                }

                //Check for named cell
                if (Tempnick == nick)
                {
                    //Assign the cells to varibales
                    CCode = (string)row[0];
                    CName = (string)row[1];
                    CSub = (string)row[2];
                    CCost = (string)row[3];
                    CEnergy = (string)row[4];
                    CRarity = (string)row[5];
                    CAffiliation = (string)row[6];
                    CEffect = (string)row[7];
                    CStat = (string)row[8];
                    CImage = (string)row[9];
                    CFImage = (string)row[10];
                    found = true;
                    await contextbuilder(2);
                }
            }
            if (found == false)
            {
                await Error("card", nick);
            }
        }

        [Command("promo")]
        public async Task Promo(string name)
        {
            bool found = false;
            string sheetref = "PROMO!";
            GoogleTest.Main(sheetref);
            var test = GoogleTest._values;

            name = name.ToLower();

            foreach (var row in test)
            {
                Tempname = (string)row[1];
                Tempname = Tempname.ToLower();

                holder = (string)row[1];
                if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                {
                    if (holder.Contains(' '))
                    {
                        holder = "\"" + holder + "\"";

                    }
                    valid.Add(holder);
                }

                //Check for named cell
                if ((Tempname == name))
                {
                    //Assign the cells to varibales
                    CCode = (string)row[0];
                    CName = (string)row[1];
                    CSub = (string)row[2];
                    CCost = (string)row[3];
                    CEnergy = (string)row[4];
                    CRarity = (string)row[5];
                    CAffiliation = (string)row[6];
                    CEffect = (string)row[7];
                    CStat = (string)row[8];
                    CImage = (string)row[9];
                    CFImage = (string)row[10];
                    found = true;
                    await contextbuilder(2);
                }
            }
            if (found == false)
            {
                await Error("card", name);
            }
        }

        [Command("promo")]
        public async Task FindPromo(string set, string name)
        {
            bool found = false;
            string sheetref = "PROMO!";
            GoogleTest.Main(sheetref);
            var test = GoogleTest._values;

            set = set.ToLower();
            name = name.ToLower();

            foreach (var row in test)
            {
                Tempset = (string)row[0];
                Tempset = Tempset.ToLower();

                Tempname = (string)row[1];
                Tempname = Tempname.ToLower();

                holder = (string)row[1];
                if ((!valid.Contains(holder)) && (!valid.Contains("\"" + holder + "\"")))
                {
                    if (holder.Contains(' '))
                    {
                        holder = "\"" + holder + "\"";

                    }
                    valid.Add(holder);
                }

                //Check for named cell
                if ((Tempset == set) && (Tempname == name))
                {
                    //Assign the cells to varibales
                    CCode = (string)row[0];
                    CName = (string)row[1];
                    CSub = (string)row[2];
                    CCost = (string)row[3];
                    CEnergy = (string)row[4];
                    CRarity = (string)row[5];
                    CAffiliation = (string)row[6];
                    CEffect = (string)row[7];
                    CStat = (string)row[8];
                    CImage = (string)row[9];
                    CFImage = (string)row[10];
                    found = true;
                    await contextbuilder(2);
                }
            }
            if (found == false)
            {
                await Error("card", name);
            }
        }

        [Command("keyword")]
        public async Task keyword(string word)
        {
            
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://wizkids.com/dicemasters/keywords/");
            File.WriteAllText("TextFiles/keywordscrape.txt", downloadString);
            string fixedtext = "";
            string loadtext = File.ReadAllText("TextFiles/keywordscrape.txt");
            word = Upper(word);
            int indexoffirst = loadtext.IndexOf(word);
            if (indexoffirst >= 0)
            {
                indexoffirst += word.Length;
                int indexofseccond = loadtext.IndexOf("</p>", indexoffirst);
                if (indexofseccond >= 0)
                {
                    fixedtext = loadtext.Substring(indexoffirst, indexofseccond - indexoffirst);
                }
                else
                {
                    fixedtext = loadtext.Substring(indexoffirst);
                }
                fixedtext = fixedtext.Replace("â€™", "'");
                fixedtext = fixedtext.Replace("&#8217;", "'");
                fixedtext = fixedtext.Replace("Â", "");
                fixedtext = RemoveBetween(fixedtext, '<', '>');
                fixedtext = fixedtext.Substring(fixedtext.IndexOf(">") + 1);
                fixedtext = fixedtext.Substring(fixedtext.IndexOf(":") + 2);
            }


            File.WriteAllText("TextFiles/keyword.txt", fixedtext);

            string result = File.ReadAllText("TextFiles/keyword.txt");

            await Context.Channel.SendMessageAsync("**" + word + ": **" + result);


        }

        [Command("keyword")]
        public async Task keyword(string word, string word2)
        {

            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://wizkids.com/dicemasters/keywords/");
            File.WriteAllText("TextFiles/keywordscrape.txt", downloadString);
            string fixedtext = "";
            string loadtext = File.ReadAllText("TextFiles/keywordscrape.txt");
            word = Upper(word);
            word2 = Upper(word2);
            string combined = word + " " + word2;
            int indexoffirst = loadtext.IndexOf(word + " " + word2);
            if (indexoffirst >= 0)
            {
                indexoffirst += combined.Length;
                int indexofseccond = loadtext.IndexOf("</p>", indexoffirst);
                if (indexofseccond >= 0)
                {
                    fixedtext = loadtext.Substring(indexoffirst, indexofseccond - indexoffirst);
                }
                else
                {
                    fixedtext = loadtext.Substring(indexoffirst);
                }
                fixedtext = fixedtext.Replace("â€™", "'");
                fixedtext = fixedtext.Replace("&#8217;", "'");
                fixedtext = fixedtext.Replace("Â", "");
                fixedtext = RemoveBetween(fixedtext, '<', '>');
                fixedtext = fixedtext.Substring(fixedtext.IndexOf(">") + 1);
                fixedtext = fixedtext.Substring(fixedtext.IndexOf(":") + 2);
            }


            File.WriteAllText("TextFiles/keyword.txt", fixedtext);

            string result = File.ReadAllText("TextFiles/keyword.txt");

            await Context.Channel.SendMessageAsync("**" + combined + ": **" + result);


        }

        [Command("keyword")]
        public async Task keyword(string word, string word2, string word3)
        {

            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://wizkids.com/dicemasters/keywords/");
            File.WriteAllText("TextFiles/keywordscrape.txt", downloadString);
            string fixedtext = "";
            string loadtext = File.ReadAllText("TextFiles/keywordscrape.txt");
            word = Upper(word);
            word2 = word2.ToLower();
            word3 = Upper(word3);
            string combined = word + " " + word2 + " " + word3;
            int indexoffirst = loadtext.IndexOf(combined);
            if (indexoffirst >= 0)
            {
                indexoffirst += combined.Length;
                int indexofseccond = loadtext.IndexOf("</p>", indexoffirst);
                if (indexofseccond >= 0)
                {
                    fixedtext = loadtext.Substring(indexoffirst, indexofseccond - indexoffirst);
                }
                else
                {
                    fixedtext = loadtext.Substring(indexoffirst);
                }
                fixedtext = fixedtext.Replace("â€™", "'");
                fixedtext = fixedtext.Replace("&#8217;", "'");
                fixedtext = fixedtext.Replace("Â", "");
                fixedtext = RemoveBetween(fixedtext, '<', '>');
                fixedtext = fixedtext.Substring(fixedtext.IndexOf(">") + 1);
                fixedtext = fixedtext.Substring(fixedtext.IndexOf(":") + 2);
            }


            File.WriteAllText("TextFiles/keyword.txt", fixedtext);

            string result = File.ReadAllText("TextFiles/keyword.txt");

            await Context.Channel.SendMessageAsync("**" + combined + ": **" + result);


        }

        [Command("fullpic")]
        public async Task DisplaySet(string value)
        {
            string[] sets = setlines.ToArray();
            string sheet = value;
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";

            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
            }
            else
            {
               
                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                foreach(var row in test)
                {
                    CCode = (string)row[0];
                    CName = (string)row[1];
                    CSub = (string)row[2];
                    CCost = (string)row[3];
                    CEnergy = (string)row[4];
                    CRarity = (string)row[5];
                    CAffiliation = (string)row[6];
                    CEffect1 = (string)row[7];
                    CEffect = CEffect1;
                    CStat = (string)row[8];
                    CImage = (string)row[9];
                    CFImage = (string)row[10];
                    await PMbuilder(2);
                }
            }
        }

        [Command("fulltext")]
        public async Task Displaytext(string value)
        {
            string[] sets = setlines.ToArray();
            string sheet = value;
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";

            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
            }
            else
            {

                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                foreach (var row in test)
                {
                    CCode = (string)row[0];
                    CName = (string)row[1];
                    CSub = (string)row[2];
                    CCost = (string)row[3];
                    CEnergy = (string)row[4];
                    CRarity = (string)row[5];
                    CAffiliation = (string)row[6];
                    CEffect1 = (string)row[7];
                    CEffect = CEffect1;
                    CStat = (string)row[8];
                    CImage = (string)row[9];
                    CFImage = (string)row[10];
                    await PMbuilder(1);
                }
            }
        }

        [Command("sub")]
        public async Task FindSub(string sheet, string name)
        {
            string[] sets = setlines.ToArray();
            bool found = false;
            sheet = sheet.ToUpper();

            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
                found = true;
            }
            else
            {

                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                name = name.ToLower();
                int count = 0;
                foreach (var row in test)
                {
                    Tempname = (string)row[2];
                    Tempname = Tempname.ToLower();

                    //Check for named cell
                    if (Tempname == name)
                    {
                        count++;
                    }
                }

                foreach (var row in test)
                {
                    Tempname = (string)row[2];
                    Tempname = Tempname.ToLower();

                    //Check for named cell
                    if (Tempname == name)
                    {
                        //Assign the cells to varibales
                        CCode = (string)row[0];
                        CName = (string)row[1];
                        CSub = (string)row[2];
                        CCost = (string)row[3];
                        CEnergy = (string)row[4];
                        CRarity = (string)row[5];
                        CAffiliation = (string)row[6];
                        CEffect = (string)row[7];
                        CStat = (string)row[8];
                        CImage = (string)row[9];
                        CFImage = (string)row[10];

                        found = true;
                        if (count > 1)
                        {
                            await contextbuilder(1);
                        }
                        else
                        {
                            await contextbuilder(2);
                        }
                    }
                }
            }
            if (found == false)
            {
                await Error("set", sheet);
            }
        }

        [Command("super")]
        public async Task DisplaySupers(string set)
        {
            string[] sets = setlines.ToArray();
            bool found = false;
            string sheet = set;
            sheet = sheet.ToUpper();

            if (sheet == "TMT")
                sheet = "THOR";

            if (!sets.Contains(sheet.ToUpper()))
            {
                await Error("set", sheet);
                found = true;
            }
            else
            {              
                string sheetref = sheet + "!";
                GoogleTest.Main(sheetref);
                var test = GoogleTest._values;

                foreach (var row in test)
                {
                    //Check for named cell
                    if ((string)row[5] == "Super")
                    {

                        //Assign the cells to varibales
                        CCode = (string)row[0];
                        CName = (string)row[1];
                        CSub = (string)row[2];
                        CCost = (string)row[3];
                        CEnergy = (string)row[4];
                        CRarity = (string)row[5];
                        CAffiliation = (string)row[6];
                        CEffect1 = (string)row[7];
                        CEffect = CEffect1;
                        CStat = (string)row[8];
                        CImage = (string)row[9];
                        CFImage = (string)row[10];
                        found = true;

                        await contextbuilder(1);
                    }
                }
                                
            }

            if (found == false)
            {
                await Context.Channel.SendFileAsync("hall.png");
                await Context.Channel.SendMessageAsync("Oh dear," + Context.User.Mention + "\n\n I don't appear to have any super rares in that set.");
            }
        }


        private string randomNumber(string sheet)
        {

            Random number = new Random(Guid.NewGuid().GetHashCode());
            int tempaid;
            int temp;
            string tempstring;

            if (sheet == "AOU") //146
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 75);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(75, 107);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(107, 147);
                }
                else
                    temp = number.Next(1, 147);


                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if ((sheet == "ASM") || (sheet == "CW") || (sheet == "FUS") || (sheet == "WF") || (sheet == "WOL")) //142
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 75);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(75, 107);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(107, 143);
                }
                else
                    temp = number.Next(1, 143);

                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if (sheet == "YGO") //120
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 41);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(41, 71);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(71, 121);
                }
                else
                    temp = number.Next(1, 121);

                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if (sheet == "UXM") //120
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 41);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(41, 71);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(71, 127);
                }
                else
                    temp = number.Next(1, 127);

                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if ((sheet == "BAT") || (sheet == "DP") || (sheet == "GAF") || (sheet == "GOTG")) //124
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 41);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(41, 81);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(81, 125);
                }
                else
                    temp = number.Next(1, 125);

                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if (sheet == "AVX") //134
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 65);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(65, 99);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(99, 133);
                }
                else
                    temp = number.Next(1, 133);

                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if (sheet == "BFF" || (sheet == "DP")) //138
            {
                tempaid = number.Next(1, 4);
                if (tempaid == 1)
                {
                    temp = number.Next(1, 65);
                }
                else if (tempaid == 2)
                {
                    temp = number.Next(65, 99);
                }
                else if (tempaid == 3)
                {
                    temp = number.Next(99, 139);
                }
                else
                    temp = number.Next(1, 139);

                if (temp < 10)
                    tempstring = "00" + temp.ToString();
                else if ((temp < 100) && (temp >= 10))
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if ((sheet == "SMC") || (sheet == "DEF") || (sheet == "DRS") || (sheet == "SMC")) //24
            {
                temp = number.Next(1, 25);
                if (temp < 10)
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else if ((sheet == "SWW") || (sheet == "IMW")) //34
            {
                temp = number.Next(1, 35);
                if (temp < 10)
                    tempstring = "0" + temp.ToString();
                else
                    tempstring = temp.ToString();
            }
            else
                tempstring = "";

            return tempstring;
                
        }

        private async Task<string> randomSheet(int number)
        {
            string[] sets = setlines.ToArray();
            await Task.Delay(1);
            return sets[number];
        }

        private async Task contextbuilder(int type)
        {
            if (CName == "Imprisoned")
            {
                await Context.Channel.SendMessageAsync("... ... Just why would you want to know about THIS card?! Ugh, you humans disgust me.");
                System.Threading.Thread.Sleep(2000);
            
            }


            if (CRarity == "Common")
            {
                rarity = new Color(165, 162, 159);
            }
            else if (CRarity == "Uncommon")
            {
                rarity = new Color(56, 181, 0);
            }
            else if (CRarity == "Rare")
            {
                rarity = new Color(255, 255, 93);
            }
            else if (CRarity == "Super")
            {
                rarity = new Color(255, 0, 0);
            }
            else if (CRarity == "Promo")
            {
                rarity = new Color(100, 211, 249);
            }
            else if (CRarity == "Chase")
            {
                rarity = new Color(218, 40, 176);
            }
            else
            {
                rarity = new Color(0, 0, 0);
            }

            EmbedBuilder MyEmbedBuilder = new EmbedBuilder();
            MyEmbedBuilder.WithColor(rarity);
            MyEmbedBuilder.WithTitle(CName);
            MyEmbedBuilder.WithDescription(CSub);
            if (type == 2) MyEmbedBuilder.WithImageUrl(CImage);

            EmbedFooterBuilder MyFooterBuilder = new EmbedFooterBuilder();
            MyFooterBuilder.WithText(CStat + " -- " + CCode);
            MyFooterBuilder.WithIconUrl(CFImage);
            MyEmbedBuilder.WithFooter(MyFooterBuilder);

            EmbedFieldBuilder MyEmbedField = new EmbedFieldBuilder();
            MyEmbedField.WithIsInline(true);
            MyEmbedField.WithName("__Cost__");
            CEnergy = EffectEmojiInsert(CEnergy);
            MyEmbedField.WithValue(CCost + " " + CEnergy);
            MyEmbedBuilder.AddField(MyEmbedField);

            EmbedFieldBuilder MyEmbedField2 = new EmbedFieldBuilder();
            MyEmbedField2.WithIsInline(true);
            MyEmbedField2.WithName("__Affiliation__");
            CAffiliation = AffiliationReplacer(CAffiliation);
            MyEmbedField2.WithValue(CAffiliation);
            MyEmbedBuilder.AddField(MyEmbedField2);

            if (CEffect.Contains("Global:"))
            {
                global = true;
                CGlobal = CEffect.Substring(CEffect.IndexOf("Global:") + 7);
                CEffect = CEffect.Substring(0, CEffect.IndexOf("Global:"));
                if (CEffect == "")
                {
                    CEffect = "None.";
                } 
            }

            if (global == true)
            {
                EmbedFieldBuilder MyEmbedField3 = new EmbedFieldBuilder();
                MyEmbedField3.WithName("__Effect__");
                CEffect = AffiliationReplacer(CEffect);
                MyEmbedField3.WithValue(CEffect);
                MyEmbedBuilder.AddField(MyEmbedField3);

                EmbedFieldBuilder MyEmbedField4 = new EmbedFieldBuilder();
                MyEmbedField4.WithName("__Global__");
                CGlobal = AffiliationReplacer(CGlobal);
                MyEmbedField4.WithValue(CGlobal);
                MyEmbedBuilder.AddField(MyEmbedField4);

                global = false;
            }
            else
            {
                EmbedFieldBuilder MyEmbedField3 = new EmbedFieldBuilder();
                MyEmbedField3.WithName("__Effect__");
                CEffect = AffiliationReplacer(CEffect);
                MyEmbedField3.WithValue(CEffect);
                MyEmbedBuilder.AddField(MyEmbedField3);
            }

            await Context.Channel.SendMessageAsync("", false, MyEmbedBuilder.Build());
        }
        private async Task globalprint(string name)
        {

           
            EmbedBuilder MyEmbedBuilder = new EmbedBuilder();
            MyEmbedBuilder.WithColor(0xff9115);
            MyEmbedBuilder.WithTitle(name + "'s Global Abilities:" );

            EmbedFieldBuilder MyEmbedField4 = new EmbedFieldBuilder();
            MyEmbedField4.WithName("__Global__");
            CGlobal = AffiliationReplacer(CGlobal);
            MyEmbedField4.WithValue(CGlobal);
            MyEmbedBuilder.AddField(MyEmbedField4);


            await Context.Channel.SendMessageAsync("", false, MyEmbedBuilder.Build());
        }

        private async Task PMbuilder(int type)
        {

            CEffect = CEffect.Replace("\n", Environment.NewLine);


            if (CRarity == "Common")
            {
                rarity = new Color(165, 162, 159);
            }
            else if (CRarity == "Uncommon")
            {
                rarity = new Color(56, 181, 0);
            }
            else if (CRarity == "Rare")
            {
                rarity = new Color(255, 255, 93);
            }
            else if (CRarity == "Super")
            {
                rarity = new Color(255, 0, 0);
            }
            else if (CRarity == "Promo")
            {
                rarity = new Color(100, 211, 249);
            }
            else if (CRarity == "Chase")
            {
                rarity = new Color(218, 40, 176);
            }
            else
            {
                rarity = new Color(0, 0, 0);
            }

            EmbedBuilder MyEmbedBuilder = new EmbedBuilder();
            MyEmbedBuilder.WithColor(rarity);
            MyEmbedBuilder.WithTitle(CName);
            MyEmbedBuilder.WithDescription(CSub);
            if(type == 2) MyEmbedBuilder.WithImageUrl(CImage);

            EmbedFooterBuilder MyFooterBuilder = new EmbedFooterBuilder();
            MyFooterBuilder.WithText(CStat + " -- " + CCode);
            MyFooterBuilder.WithIconUrl(CFImage);
            MyEmbedBuilder.WithFooter(MyFooterBuilder);

            EmbedFieldBuilder MyEmbedField = new EmbedFieldBuilder();
            MyEmbedField.WithIsInline(true);
            MyEmbedField.WithName("__Cost__");
            CEnergy = EffectEmojiInsert(CEnergy);
            MyEmbedField.WithValue(CCost + " " + CEnergy);
            MyEmbedBuilder.AddField(MyEmbedField);

            EmbedFieldBuilder MyEmbedField2 = new EmbedFieldBuilder();
            MyEmbedField2.WithIsInline(true);
            MyEmbedField2.WithName("__Affiliation__");
            CAffiliation = AffiliationReplacer(CAffiliation);
            MyEmbedField2.WithValue(CAffiliation);
            MyEmbedBuilder.AddField(MyEmbedField2);

            if (CEffect.Contains("Global:"))
            {
                global = true;
                CGlobal = CEffect.Substring(CEffect.IndexOf("Global:") + 7);
                CEffect = CEffect.Substring(0, CEffect.IndexOf("Global:"));
            }

            if (global == true)
            {
                EmbedFieldBuilder MyEmbedField3 = new EmbedFieldBuilder();
                MyEmbedField3.WithName("__Effect__");
                CEffect = AffiliationReplacer(CEffect);
                MyEmbedField3.WithValue(CEffect);
                MyEmbedBuilder.AddField(MyEmbedField3);

                EmbedFieldBuilder MyEmbedField4 = new EmbedFieldBuilder();
                MyEmbedField4.WithName("__Global__");
                CEffect = AffiliationReplacer(CGlobal);
                MyEmbedField4.WithValue(CGlobal);
                MyEmbedBuilder.AddField(MyEmbedField4);

                global = false;
            }
            else
            {
                EmbedFieldBuilder MyEmbedField3 = new EmbedFieldBuilder();
                MyEmbedField3.WithName("__Effect__");
                CEffect = AffiliationReplacer(CEffect);
                MyEmbedField3.WithValue(CEffect);
                MyEmbedBuilder.AddField(MyEmbedField3);
            }

            await Context.User.SendMessageAsync("", false, MyEmbedBuilder.Build());
        }

        public async Task Error(string type, string erronious)
        {
            string[] sets = setlines.ToArray();
            string[] setname = setnamelines.ToArray();

            var activeuser = Context.User;
            string errorType = "";
            string errorMessage = "";
            string normalError = "";
            //Color rarity = new Color(218, 40, 176);

            if (type == "set")
            {
                Dictionary<string, int> resultset = new Dictionary<string, int>();
                foreach (string stringtoTest in sets)
                {
                    resultset.Add(stringtoTest, LevenshteinDistance.Compute(erronious, stringtoTest));
                }
               
                int minimumModifications = resultset.Min(c => c.Value);


                string s = "Could you have been looking for the following?  : \n";

                foreach (KeyValuePair<string, int> pair in resultset)
                {
                    if (pair.Value == minimumModifications)
                    {
                        int index = Array.IndexOf(sets, pair.Key);
                        string nameOfSet = setname[index];

                        s += pair.Key + " | " + nameOfSet + "\n";

                    }
                }

                errorType = "Unknown Set";
                errorMessage = "My, my, my, " + Context.User.Username + ". I do not have a record of that set in my collection. \n Please type \">sets\" to have" +
                    " a list of sets and their codes to you via private message. Alternatively you can type \">setshere\" to have the list printed out in the current channel. \n\n" + s;


                normalError = "the set";
            }
            else if (type == "card")
            {


                errorType = "Unknown Card/Character";
                string s = "";
                for (int i = 0; i < valid.Count; i++)
                {
                    if (i == valid.Count - 1)
                    {
                        s += valid[i];
                    }
                    else
                    {
                        s += valid[i] + ", ";
                    }
                }

                Dictionary<string, int> resultset = new Dictionary<string, int>();
                foreach (string stringtoTest in valid)
                {
                    resultset.Add(stringtoTest, LevenshteinDistance.Compute(erronious, stringtoTest));
                }

                int minimumModifications = resultset.Min(c => c.Value);


                s += "\n\nCould you have been looking for the following? : \n";

                foreach (KeyValuePair<string, int> pair in resultset)
                {
                    if (pair.Value == minimumModifications)
                    {
                        int intex = valid.IndexOf(pair.Key);
                       
                        s += pair.Key + "\n";

                    }

                }

                errorMessage = ("My, my, my, " + Context.User.Username + "\n\n I don't appear to have that card recoreded. \n\n Here are the valid cards in the given set :\n " + s + "\n\nIf you copy and paste, make sure to include any quotation marks.");
                normalError = "the card name";

            }
            else if (type == "rarity")
            {
                errorType = "Rarity";
                errorMessage = "At least there was an attempt" + Context.User.Username + "... Here is how to do the rarities. \n\nFor Common cards type : c or Common. \nFor Uncommon cards type : uc, u or Uncommon. \nFor Rare cards type : r or Rare" +
                    "\nFor Super Rare cards type: s, sr, Super or 'Super Rare'. \nFor promo cards type: p or Promo. \nAnd finally, for Chase cards type: ch or Chase.";
                normalError = "the rarity";
            }
            else if (type == "global")
            {
                errorType = "Global";
                errorMessage = "*sigh*.  " + Context.User.Username + " You do know that that character does not have a global, right?";
                normalError = "the globals on that character";
            }
            else if (type == "dice")
            {
                errorType = "teambuildergif";
                errorMessage = "*hmmm*.  " + Context.User.Username + " How about trying one of the following 4 options. Dice or die for the dice quantites, or nodice or nodie for images without quantites?";
                normalError = "the quantiy graphic variable";
            }
            else
            {
                errorType = "???";
                errorMessage = "???";
            }

            EmbedBuilder EmbeddedError = new EmbedBuilder();
            EmbeddedError.WithColor(218, 40, 176);
            EmbeddedError.WithTitle("__ERROR!__");
            EmbeddedError.WithDescription(errorType);
            EmbeddedError.WithImageUrl("https://i.imgur.com/Ov6CJML.jpg");

            EmbedFieldBuilder ErrorMessageField = new EmbedFieldBuilder();
            ErrorMessageField.WithIsInline(true);
            ErrorMessageField.WithName("A Message from Brainiac");
            ErrorMessageField.WithValue(errorMessage);
            EmbeddedError.AddField(ErrorMessageField);

            await Context.Channel.SendMessageAsync("There is an error with  " + normalError + ". I have PM'ed you more details.");
            await activeuser.SendMessageAsync("", false, EmbeddedError.Build());
         
        }

        public static string RemoveBetween(string s, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }

        public static string Upper(string s)
        {
            s.ToLower();
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string AffiliationReplacer(string s)
        {
            string[] Affiliations = affiliationlines.ToArray();
            string[] AffiliationEmoji = AffiliationEmojilines.ToArray();
            string[] keywords = keywordlines.ToArray();

            for (int i = 0; i < Affiliations.Length; i++)
            {
                if(s.Contains(Affiliations[i]))
                {
                    s = s.Replace(Affiliations[i], AffiliationEmoji[i]);
                }
            }

            for (int i = 0; i < keywords.Length; i++)
            {
                if(s.Contains(keywords[i]))
                {
                    if (i == 4)
                    {
                        s = s.Replace("Anti-Breath Weapon", "**Anti-Breath Weapo**");
                    }
                    else
                    {
                        s = s.Replace(keywords[i], "**" + keywords[i] + "**");
                    }
                    
                }

            }

            s = s.Replace("**Anti-Breath Weapo**", "**Anti-Breath Weapon**");

            if (s.Contains("Avenger") && (!s.Contains("<:Avengers:336237385547513856>")))
            {
                s = s.Replace("Avenger", "<:Avengers:336237385547513856>");
            }

            if (s.Contains("Villain")|| s.Contains("Villains") || s.Contains("villain") || s.Contains("villains"))
            {
                s = s.Replace("villain", "<:villain:336232473862340614>");
                s = s.Replace("Villains", "<:villain:336232473862340614>");
                s = s.Replace("villains", "<:villain:336232473862340614>");
                s = s.Replace("Villain", "<:villain:336232473862340614>");
            }

            if (s.Contains("Fist"))
            {
                if(CName == "Iron Fist")
                {

                }
                else
                {
                    s = s.Replace("Fist", "<:Fist:366516545284866048>");
                }
            }

            if (s.Contains("Mask"))
            {
               s = s.Replace("Mask", "<:Mask:366516573466394624>");
            }

            if (s.Contains("Bolt"))
            {
                if ((CName == "Black Bolt") || (CName == "King Black Bolt"))
                {

                }
                else
                {
                    s = s.Replace("Bolt", "<:Bolt:366516620522160128>");
                }
            }

            if (s.Contains("Shield"))
            {
                if ((CName == "Vibranium Shield"))
                {

                }
                else
                {
                    s = s.Replace("Shield", "<:Shield:366516603027980288>.");
                }
            }
            return s;
        }

        public static string EffectEmojiInsert(string s)
        {
            if(s == "Fist")
            {
                s = "<:Fist:366516545284866048>";
            }
            else if (s == "Shield")
            {
                s = "<:Shield:366516603027980288>";
            }
            else if (s == "Mask")
            {
                s = "<:Mask:366516573466394624>";
            }
            else if (s == "Bolt")
            {
                s = "<:Bolt:366516620522160128>";
            }
            return s;
        }


    }

    static class LevenshteinDistance
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}
