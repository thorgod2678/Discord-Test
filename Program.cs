using DiscordRPC;

namespace DiscordTest
{
    public class Program
    {
        public static string clientID = new(" ");// = new string("1448251972934307851");
        public static string Details = new(" ");
        public static string State = new(" ");
        public static Timestamps TimeStamp = new Timestamps();
        public static List<string> IMGkeys = new List<string>();
        public static List<DiscordRPC.Button> Buttons = new List<DiscordRPC.Button>();
        public static DiscordRPC.Assets IMGasset = new DiscordRPC.Assets();
        public static int CUR_LIK_INDEX = 0;
        public static int CUR_SIK_INDEX = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Discord Test");

            Console.Write("Write your Client ID: ");
            clientID = Console.ReadLine();
            var rpc = new DiscordRpcClient(clientID);
            rpc.Initialize();

            

            Console.Write("Write your Details: ");
            Details = Console.ReadLine();
            Console.Write("Write your State: ");
            State = Console.ReadLine();

            rpc.SetPresence(new RichPresence()
            {
                Details = Details,
                State = State,
                Timestamps = TimeStamp
            });

            Console.WriteLine("Rich Presence is running. Type 'QUIT' to quit. To apply commands type 'APPLY'. Type 'HELP' for help\n\n");
            while (true) 
            {
                Console.WriteLine("Enter Command: ");
                string Input = Console.ReadLine().ToUpper();

                if (Input == "CHANGE DETAILS")
                {
                    Console.Write("\nWrite your Details: ");
                    Details = Console.ReadLine();

                   
                }
                else if(Input == "CHANGE STATE")
                {
                    Console.Write("\nWrite your State: ");
                    State = Console.ReadLine();

                    
                }
                else if(Input == "CLEAR")
                {
                    rpc.ClearPresence();
                    Console.WriteLine("Cleared..\n");
                }
                else if(Input == "APPLY")
                {
                    rpc.SetPresence(new RichPresence()
                    {
                        Details = Details,
                        State = State,
                        Timestamps = TimeStamp,
                        Buttons = Buttons.ToArray(),

                        Assets = IMGasset

                    });

                    Console.WriteLine("Applied..");
                }
                else if(Input == "ADD BUTTON")
                {
                    
                    Console.Write("\nWrite your Button's Name: ");
                    string name = Console.ReadLine();

                    Console.Write("\nWrite your Button's URL: ");
                    string url = Console.ReadLine();

                    if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    {
                        url = "https://" + url;
                    }

                    DiscordRPC.Button NBUT = new DiscordRPC.Button() { Label = name, Url= url};
                    Buttons.Add(NBUT);
                }
                else if(Input == "TEST")
                {
                    rpc.SetPresence(new RichPresence()
                    {
                        Details = "TESTING BUTTONS",
                        State = "Should show button below",
                        Assets = new Assets()
                        {
                            LargeImageKey = "shrek",
                            
                            
                            
                        },
                        Buttons = new[]
                        {
                            new DiscordRPC.Button()
                            {
                                Label = "Google",
                                Url = "https://google.com"
                            }
                        }
                    });

                }
                else if(Input == "ADD KEY")
                {
                    Console.Write("\nWrite your Key's Name: ");
                    string key = Console.ReadLine();

                    IMGkeys.Add(key);
                    Console.WriteLine("Added key '" + key + "'");
                }
                else if(Input == "VIEW KEY")
                {
                    Console.WriteLine("Your Keys are:");
                    int index = 0;
                    foreach(string x in IMGkeys)
                    {
                        
                        Console.WriteLine(" "+index.ToString()+". "+x);
                        index++;
                    }
                }
                else if(Input == "SET IMG")
                {
                    Console.WriteLine("Enter the index of your LargeImageKey and SmallImageKey (Can be obtained from 'VIEW_KEY'): ");
                    Console.Write("Large Image Key: ");
                    if (!int.TryParse(Console.ReadLine(), out int lik) || lik < 0 || lik >= IMGkeys.Count)
                    {
                        Console.WriteLine("Invalid Large Image index.");
                        continue;
                    }
                    Console.Write("Small Image Key: ");
                    if (!int.TryParse(Console.ReadLine(), out int sik) || sik < 0 || sik >= IMGkeys.Count)
                    {
                        Console.WriteLine("Invalid Small Image index.");
                        continue;
                    }
                    Console.WriteLine("Enter the other details (Can be left blank): ");
                    Console.Write("Enter the url for Large Image: ");
                    string liu = Console.ReadLine();
                    Console.Write("Enter the text for Large Image: ");
                    string lit = Console.ReadLine();
                    Console.Write("Enter the url for Small Image: ");
                    string siu = Console.ReadLine();
                    Console.Write("Enter the text for Small Image: ");
                    string sit = Console.ReadLine();

                    IMGasset = new DiscordRPC.Assets
                    {
                        LargeImageKey = IMGkeys[Convert.ToInt32(lik)],
                        LargeImageText = lit,
                        LargeImageUrl = liu,
                        SmallImageKey = IMGkeys[Convert.ToInt32(sik)],
                        SmallImageText = sit,
                        SmallImageUrl = siu
                    };

                    CUR_LIK_INDEX = Convert.ToInt32(lik);
                    CUR_SIK_INDEX = Convert.ToInt32(sik);

                    Console.WriteLine("Set Image Asset Sucsessfully...");

                }
                else if(Input == "PREVIEW")
                {
                    
                    Console.WriteLine("\n\nPreview: ");
                    Console.WriteLine("Details: " + Details);
                    Console.WriteLine("State: " + State);
                    Console.WriteLine("TimeStamp: " + TimeStamp);
                    if (IMGkeys.Count == 0)
                    {
                        Console.WriteLine("No image keys set.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Large Image Key: " + CUR_LIK_INDEX + ". " + IMGkeys[Convert.ToInt32(CUR_LIK_INDEX)]);
                        Console.WriteLine("Small Image Key: " + CUR_SIK_INDEX + ". " + IMGkeys[Convert.ToInt32(CUR_SIK_INDEX)]);
                    }
                    Console.WriteLine("Buttons: ");
                    int y = 0;
                    foreach(var x in Buttons)
                    {
                        Console.WriteLine(" Button no. " + y.ToString());
                        Console.WriteLine(" Button's URL: " + x.Url + " Buttons Label: " + x.Label);
                    }
                }
                else if (Input == "SET TIMESTAMP ELAPSED")
                {
                    TimeStamp = Timestamps.Now;
                    Console.WriteLine("Timestamp set: elapsed time from now.");
                }

                else if (Input.StartsWith("SET TIMESTAMP COUNTDOWN"))
                {
                    string[] parts = Input.Split(' ');
                    if (parts.Length < 4 || !int.TryParse(parts[3], out int minutes))
                    {
                        Console.WriteLine("Usage: SET TIMESTAMP COUNTDOWN <minutes>");
                    }
                    else
                    {
                        TimeStamp = new Timestamps
                        {
                            End = DateTime.UtcNow.AddMinutes(minutes)
                        };
                        Console.WriteLine($"Timestamp set: countdown of {minutes} minutes.");
                    }
                }

                else if (Input.StartsWith("SET TIMESTAMP DURATION"))
                {
                    string[] parts = Input.Split(' ');
                    if (parts.Length < 4 || !int.TryParse(parts[3], out int minutes))
                    {
                        Console.WriteLine("Usage: SET TIMESTAMP DURATION <minutes>");
                    }
                    else
                    {
                        TimeStamp = new Timestamps
                        {
                            Start = DateTime.UtcNow,
                            End = DateTime.UtcNow.AddMinutes(minutes)
                        };
                        Console.WriteLine($"Timestamp set: duration of {minutes} minutes.");
                    }
                }

                else if (Input == "CLEAR TIMESTAMP")
                {
                   
                        TimeStamp = new Timestamps();
                        Console.WriteLine("Timestamp cleared.");
                    

                }
                else if (Input.StartsWith("SET START TIMESTAMP UTC"))
                {
                    // Expected format:
                    // SET TIMESTAMP UTC YYYY-MM-DD HH:MM
                    TimeStamp ??= new Timestamps();

                    string raw = Input.Substring("SET START TIMESTAMP UTC".Length).Trim();

                    if (!DateTime.TryParseExact(
                        raw,
                        "yyyy-MM-dd HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.AssumeUniversal,
                        out DateTime utcTime))
                    {
                        Console.WriteLine("Invalid format.");
                        Console.WriteLine("Use: SET TIMESTAMP UTC YYYY-MM-DD HH:MM");
                        Console.WriteLine("Example: SET TIMESTAMP UTC 2025-12-15 18:30");
                    }
                    else
                    {
                        TimeStamp.Start = DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);

                        Console.WriteLine($"Timestamp set to UTC time: {utcTime:yyyy-MM-dd HH:mm} (UTC)");
                    }
                }
                else if (Input.StartsWith("SET END TIMESTAMP UTC"))
                {
                    // Expected format:
                    // SET TIMESTAMP UTC YYYY-MM-DD HH:MM
                    TimeStamp ??= new Timestamps();

                    string raw = Input.Substring("SET END TIMESTAMP UTC".Length).Trim();

                    if (!DateTime.TryParseExact(
                        raw,
                        "yyyy-MM-dd HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.AssumeUniversal,
                        out DateTime utcTime))
                    {
                        Console.WriteLine("Invalid format.");
                        Console.WriteLine("Use: SET TIMESTAMP UTC YYYY-MM-DD HH:MM");
                        Console.WriteLine("Example: SET TIMESTAMP UTC 2025-12-15 18:30");
                    }
                    else
                    {
                        TimeStamp.End = DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);


                        Console.WriteLine($"Timestamp set to UTC time: {utcTime:yyyy-MM-dd HH:mm} (UTC)");
                    }
                }




                else if (Input == "HELP")
                {
                                    Console.WriteLine(@"
                                        ==================== DISCORD RPC TOOL HELP ====================

                                        This tool lets you control what Discord shows as your activity
                                        using Discord Rich Presence.

                                        IMPORTANT:
                                        - Changes are sent to Discord ONLY when you use APPLY.
                                        - Discord Desktop must be running.
                                        - ""Display current activity as status message"" must be enabled
                                          in Discord settings.

                                        ---------------------------------------------------------------

                                        BASIC COMMANDS
                                        --------------

                                        CHANGE DETAILS
                                          Change the main activity text (top line).

                                        CHANGE STATE
                                          Change the secondary activity text (bottom line).

                                        APPLY
                                          Sends the current configuration to Discord.
                                          (Always run this after making changes.)

                                        CLEAR
                                          Clears the current Discord Rich Presence.

                                        PREVIEW
                                          Shows the current configuration locally.
                                          (Does NOT send anything to Discord.)

                                        QUIT
                                          Exits the program.

                                        ---------------------------------------------------------------

                                        BUTTON COMMANDS
                                        ---------------

                                        ADD BUTTON
                                          Adds a clickable button to your Discord activity.
                                          You will be asked for:
                                            - Button name
                                            - Button URL (must start with http:// or https://)

                                        IMPORTANT:
                                        - Discord allows a MAXIMUM of 2 buttons.
                                        - If more than 2 are added, Discord will ignore the extras.
                                        - Buttons appear in the profile popup, not directly in chat.

                                        ---------------------------------------------------------------

                                        IMAGE / ASSET COMMANDS
                                        ----------------------

                                        ADD KEY
                                          Adds an image key name.
                                          The key MUST already exist in:
                                          Discord Developer Portal → Rich Presence → Art Assets

                                        VIEW KEY
                                          Shows all added image keys along with their index numbers.

                                        SET IMG
                                          Assigns Large and Small images using key indexes.
                                          You can also optionally set:
                                            - Hover text
                                            - Image URLs (mostly ignored by Discord)

                                        NOTES:
                                        - Images are loaded ONLY by key name.
                                        - Local files will NOT work.
                                        - Image URLs are optional and usually ignored.

                                        ---------------------------------------------------------------

                                        TIMESTAMP COMMANDS
                                        ------------------

                                        SET TIMESTAMP ELAPSED
                                          Shows elapsed time starting from now.
                                          Example: ""Playing for 10 minutes""

                                        SET TIMESTAMP COUNTDOWN <minutes>
                                          Shows a countdown ending after the given number of minutes.
                                          Example:
                                            SET TIMESTAMP COUNTDOWN 30

                                        SET TIMESTAMP DURATION <minutes>
                                          Shows both elapsed and remaining time.
                                          Example:
                                            SET TIMESTAMP DURATION 45

                                        CLEAR TIMESTAMP
                                          Removes any active timestamps.

                                        ---------------------------------------------------------------

                                        ADVANCED TIMESTAMP COMMANDS (UTC)
                                        ---------------------------------

                                        SET START TIMESTAMP UTC YYYY-MM-DD HH:MM
                                          Sets an exact UTC start time.

                                        SET END TIMESTAMP UTC YYYY-MM-DD HH:MM
                                          Sets an exact UTC end time.

                                        FORMAT:
                                          - 24-hour clock
                                          - Time MUST be in UTC

                                        EXAMPLE:
                                          SET START TIMESTAMP UTC 2025-12-15 18:30
                                          SET END   TIMESTAMP UTC 2025-12-15 20:00

                                        ---------------------------------------------------------------

                                        TEST COMMAND
                                        ------------

                                        TEST
                                          Sends a hardcoded test presence to verify that:
                                          - Buttons work
                                          - Image assets load correctly

                                        ---------------------------------------------------------------

                                        FINAL NOTES
                                        -----------

                                        - Presence updates may take a few seconds to appear.
                                        - Some features (especially buttons) may not appear on your own
                                          profile but ARE visible to other users.
                                        - Discord may silently ignore invalid or unsupported values.

                                        ===============================================================
                                        ");


                }
                else if (Input == "QUIT")
                {
                    Console.WriteLine("Quitting...");
                    break;
                }
            }
            rpc.Dispose();

        }
    }
}
