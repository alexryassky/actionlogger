using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Xml.XPath;


namespace ActionLogger
{
    public static class Func
    {
  

        public static string MakeReport()
        {
            XmlDocument xdRep = new XmlDocument();      
            
            XmlNode xnRoot = xdRep.CreateElement("Record");
            XmlElement xeDate = xdRep.CreateElement("Date");
            XmlElement xePexe = xdRep.CreateElement("ProgramExe");
            XmlElement xeHandle = xdRep.CreateElement("MainWindowHandle");
            XmlElement xeLeftTop = xdRep.CreateElement("LeftTop");
            XmlElement xeRightBottom = xdRep.CreateElement("RightBottom");
            XmlElement xeRight = xdRep.CreateElement("Right");
            XmlElement xeBottom = xdRep.CreateElement("Bottom");
            XmlElement xeLeft = xdRep.CreateElement("Left");
            XmlElement xeTop = xdRep.CreateElement("Top");

            xnRoot.AppendChild(xeDate);
            xnRoot.AppendChild(xePexe);
            xnRoot.AppendChild(xeHandle);
            xnRoot.AppendChild(xeLeftTop);
            xnRoot.AppendChild(xeRightBottom);
            xeLeftTop.AppendChild(xeLeft); 
            xeLeftTop.AppendChild(xeTop);
            xeRightBottom.AppendChild(xeRight);
            xeRightBottom.AppendChild(xeBottom);

            xeLeft.InnerText = ActionHost.ptWindowPoint.X.ToString(); 
            xeTop.InnerText = ActionHost.ptWindowPoint.Y.ToString();
            xeRight.InnerText = (ActionHost.ptWindowPoint.X + ActionHost.szTargetBounds.Width).ToString();
            xeBottom.InnerText = (ActionHost.ptWindowPoint.Y+ ActionHost.szTargetBounds.Height).ToString();
            xeDate.InnerText = DateTime.Now.ToString() ;
            xePexe.InnerText = ActionHost.strTargetExeName;
            xeHandle.InnerText = ActionHost.TargetWindow.ToString();
            XmlNode xnEvents = xdRep.CreateElement("Events");
            for (int i = 0; i < ActionHost.Actions.Count - 1; i++)
            {
                XmlElement xnEvent = xdRep.CreateElement("Event");
                UserAction item = ActionHost.Actions.ElementAt<UserAction>(i);
                XmlAttribute xaEventType = xdRep.CreateAttribute("EventType");
                xnEvent.SetAttributeNode(xaEventType);
                switch (item.EventType)
                {
                    case UserActionTypes.USER_MOUSE_CLICK:
                     {
                         xaEventType.Value =  "Mouse";
                         XmlElement xnMessage = xdRep.CreateElement("Message");
                         xnEvent.AppendChild(xnMessage);                         
                         XmlAttribute xaMouseButton = xdRep.CreateAttribute("Button");
                         xaMouseButton.Value = item.KeyCode.ToString();                         
                         xnMessage.InnerText = item.EventCode.Msg.ToString();
                         
                         xnMessage.SetAttributeNode(xaMouseButton);

                         XmlElement xnCoords = xdRep.CreateElement("Coords");
                         XmlElement xnMouseX = xdRep.CreateElement("X");
                         XmlElement xnMouseY = xdRep.CreateElement("Y");
                         xnMouseX.InnerText = item.MouseCoords.X.ToString();
                         xnMouseY.InnerText = item.MouseCoords.Y.ToString();
                     
                       
                         xnCoords.AppendChild(xnMouseX);
                         xnCoords.AppendChild(xnMouseY);                         
                         
                         xnEvent.AppendChild(xnCoords);
                         
                     }
                    break;
                    default:
                    {

                    }
                    break;

                }
                xnEvent.Attributes.Append(xaEventType);
                xnEvents.AppendChild(xnEvent);
                xnRoot.AppendChild(xnEvents);
                xdRep.AppendChild(xnRoot);
                
             }
             StringBuilder sbReport = new StringBuilder(444);
             XmlWriterSettings xwsSettings = new XmlWriterSettings();
             xwsSettings.Indent = true;
             xwsSettings.NewLineChars = "\r\n";
             XmlWriter xwReport = XmlWriter.Create(sbReport,xwsSettings);
             xdRep.WriteTo(xwReport);
             xwReport.Close();
             return sbReport.ToString();
        }

        public static void SaveToXML(string Report)
        {
            SaveFileDialog dlgSaveReport = new SaveFileDialog();
            dlgSaveReport.Filter = "Текстовый файл |*.txt";
            
            if (dlgSaveReport.ShowDialog() == DialogResult.OK)
            {

                FileStream fs = new FileStream(dlgSaveReport.FileName, FileMode.Create, FileAccess.ReadWrite);
                XmlDocument xReport = new XmlDocument();
                xReport.LoadXml(Report);
                XmlWriterSettings xwsSettings = new XmlWriterSettings();
                xwsSettings.Indent = true;
                xwsSettings.NewLineChars = "\r\n";
                XmlWriter xwReport = XmlWriter.Create(fs,xwsSettings);
                xReport.Save(xwReport);
                xwReport.Close();              
                fs.Close();

            }
        }

        public static void LoadFromXML(string path, ref List<UserAction> ActionList)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                XmlReader xrReport = XmlReader.Create(fs);

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xrReport);
                
              
              
                
             
                //   xdoc.SelectSingleNode("/Record/
                ActionHost.ptWindowPoint = new System.Drawing.Point(
                                                                    Convert.ToInt32(xdoc.DocumentElement.SelectSingleNode("LeftTop/Top").InnerText),
                                                                    Convert.ToInt32(xdoc.DocumentElement.SelectSingleNode("LeftTop/Left").InnerText)
                                                                    );


                ActionHost.szTargetBounds = new System.Drawing.Size(
                    Convert.ToInt32(xdoc.DocumentElement.SelectSingleNode("RightBottom/Right").InnerText) - ActionHost.ptWindowPoint.X,
                    Convert.ToInt32(xdoc.DocumentElement.SelectSingleNode("RightBottom/Bottom").InnerText) - ActionHost.ptWindowPoint.Y
                                                                    );
                ActionHost.strTargetExeName = xdoc.DocumentElement.SelectSingleNode("ProgramExe").InnerText;
                ActionHost.TargetWindow = new IntPtr(Convert.ToInt32(xdoc.DocumentElement.SelectSingleNode("MainWindowHandle").InnerText));

                XmlNodeList xnlEvents = xdoc.SelectNodes("Record/Events/Event");
               
                foreach (XmlNode xnEvent in xnlEvents)
                {
                    
                    System.Windows.Forms.Message mes = new System.Windows.Forms.Message();
                    mes.Msg = Int32.Parse(xnEvent.SelectSingleNode("Message").InnerXml);
                    int mouseX = Int32.Parse(xnEvent.SelectSingleNode("Coords/X").InnerXml);
                    int mouseY = Int32.Parse(xnEvent.SelectSingleNode("Coords/Y").InnerXml);
                    System.Drawing.Point mouseCoords = new System.Drawing.Point(mouseX, mouseY);
                    UserAction uaItem = new UserAction(mes, (uint)ActionHost.TargetWindow.ToInt32());
                    uaItem.MouseCoords = mouseCoords;
                    ActionHost.AddItem(uaItem);
                    string val = xnEvent.InnerXml;
                   
                }
                xrReport.Close();
            }
            finally
            {
                fs.Close();

            }
        }

        public static System.Drawing.Point ParseLParamToPoint(IntPtr LParam)
        {
            int X = LParam.ToInt32() & 0x0000ffff;
            int Y = (int)((LParam.ToInt32() & 0xffff0000) >> 16);
            System.Drawing.Point pt = new System.Drawing.Point(X,Y);
            return pt;
        }
    }
}
