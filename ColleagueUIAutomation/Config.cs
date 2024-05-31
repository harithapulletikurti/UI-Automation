using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text;

namespace SeleniumWebDriverTest
{
    class Config
    {
        
        public void runConfig()
        {
            try
            {
                if (!File.Exists(Constants.filePath))
                {

                    // Write configuration values to the file
                    WriteConfig(Constants.filePath, "Credentials", "Username", Constants.username);
                    WriteConfig(Constants.filePath, "Credentials", "Defaut_Username", Constants.username);
                    WriteConfig(Constants.filePath, "Credentials", "Password", Constants.password);
                    WriteConfig(Constants.filePath, "Credentials", "Default_Password", Constants.password);
                    WriteConfig(Constants.filePath, "Colleague_Website_URL", "URL", Constants.url);
                    WriteConfig(Constants.filePath, "Colleague_Website_URL", "Default_URL", Constants.url);
                    WriteConfig(Constants.filePath, "Wait_Times", "Webpage_wait_in_sec", Constants.webpagewait);
                    WriteConfig(Constants.filePath, "Wait_Times", "Default_Webpage_wait_in_sec", Constants.webpagewait);
                    WriteConfig(Constants.filePath, "Wait_Times", "KeyPress_wait_in_sec", Constants.keywait);
                    WriteConfig(Constants.filePath, "Wait_Times", "Default_KeyPress_wait_in_sec", Constants.keywait);
                    WriteConfig(Constants.filePath, "Wait_Times", "ButtonClick_wait_in_sec", Constants.clickwait);
                    WriteConfig(Constants.filePath, "Wait_Times", "Default_ButtonClick_wait_in_sec", Constants.clickwait);

                }
                // Read configuration values from the file
                ReadConfig(Constants.filePath, "Credentials", "Username");
                ReadConfig(Constants.filePath, "Credentials", "Password");
                ReadConfig(Constants.filePath, "Colleague_Website_URL", "URL");
                ReadConfig(Constants.filePath, "Wait_Times", "Webpage_wait_in_sec");
                ReadConfig(Constants.filePath, "Wait_Times", "KeyPress_wait_in_sec");
                ReadConfig(Constants.filePath, "Wait_Times", "ButtonClick_wait_in_sec");


                if (Constants.username == null)
                {
                    Constants.username = "erpadmin8";
                }
                if (Constants.password == null)
                {
                    Constants.password = "Smash123";
                }
                if (Constants.url == null)
                {
                    Constants.url = "https://miles.colleague.elluciancloud.com/UI/home/index.html?sso=true";
                }
                if (Constants.webpagewait == null)
                {
                    Constants.webpagewait = "10";
                }
                if (Constants.keywait == null)
                {
                    Constants.keywait = "3";
                }
                if (Constants.clickwait == null)
                {
                    Constants.clickwait = "2";
                }


                Console.WriteLine("username: " + Constants.username);
                Console.WriteLine("password: " + Constants.password);
                Console.WriteLine("url: " + Constants.url);
                Console.WriteLine("webpagewait: " + Constants.webpagewait);
                Console.WriteLine("keywait: " + Constants.keywait);
                Console.WriteLine("clickwait: " + Constants.clickwait);
            }
            catch  (Exception )
            {
                
                Console.WriteLine("Config File Exception");
            }
        }




        public void WriteConfig(string filePath, string section, string key, string value)
        {
            

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            if (File.Exists(filePath))
            {
                //File.Close(filePath);
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    List<string> newLines = new List<string>();

                    bool sectionExists = false;
                    bool keyExists = false;
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            if (sectionExists && !keyExists)
                            {
                                newLines.Add(key + "=" + value);
                                keyExists = true;
                            }
                            sectionExists = (line.Substring(1, line.Length - 2) == section);
                        }
                        else if (sectionExists)
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length == 2 && parts[0].Trim() == key)
                            {
                                parts[1] = value;
                                keyExists = true;
                            }
                            newLines.Add(string.Join("=", parts));
                        }
                        newLines.Add(line);
                    }

                    if (!sectionExists)
                    {
                        newLines.Add("[" + section + "]");
                        newLines.Add(key + "=" + value);
                    }

                    File.WriteAllLines(filePath, newLines);
                }
            }
        }


        public string ReadConfig(string filePath, string section, string key)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                bool sectionExists = false;
                foreach (string line in lines)
                {
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        sectionExists = (line.Substring(1, line.Length - 2) == section);
                    }
                    else if (sectionExists)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && parts[0].Trim() == key)
                        {
                            return parts[1].Trim();
                        }
                    }
                }
                
            }
            return "\0";
        }

    }
}



