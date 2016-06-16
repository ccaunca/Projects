using System;
using System.Diagnostics;
using System.Xml;

namespace SpaceInvaders
{
    public class FontManager : Manager
    {
        private static FontManager pInstance = null;
        private FontManager(int reserveSize = 26, int reserveIncrement = 10)
            : base (reserveSize, reserveIncrement)
        {
        }
        ~FontManager()
        {
            pInstance = null;
        }
        public static void Create(int reserveSize = 26, int reserveIncrement = 10)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);

            if (pInstance == null)
            {
                pInstance = new FontManager(reserveSize, reserveIncrement);
            }
        }
        private static FontManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static void Dump()
        {
            FontManager fontMan = FontManager.GetInstance();
            fontMan.BaseDump();
        }
        public static Font Find(FontName fontName, int key)
        {
            FontManager fontMan = FontManager.GetInstance();
            return (Font)fontMan.BaseFind(new Font { name = fontName, key = key });
        }
        public static Font Add(FontName fontName, int key, TextureName texName, float x, float y, float w, float h)
        {
            FontManager fontMan = FontManager.GetInstance();
            Font font = (Font)fontMan.BaseAdd();
            Debug.Assert(font != null);
            font.Set(fontName, key, texName, x, y, w, h);
            return font;
        }
        public static void AddXml(String assetName, FontName fontName, TextureName texName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element :
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;
                    case XmlNodeType.EndElement :
                        if (reader.Name == "character")
                        {
                            FontManager.Add(fontName, key, texName, x, y, width, height);
                        }
                        break;
                }
            }
        }
        public static void Draw()
        {
            String strScore1 = ScoreManager.GetScore(ScoreType.Player1Score).ToString().PadLeft(4, '0');
            String strScore2 = ScoreManager.GetScore(ScoreType.Player2Score).ToString().PadLeft(4, '0');
            String strHiScore = ScoreManager.GetScore(ScoreType.HiScore).ToString().PadLeft(4, '0');
            String strCredits = ScoreManager.GetScore(ScoreType.Credits).ToString().PadLeft(2, '0');
            DrawString("SCORE< 1 >          HI-SCORE          SCORE< 2 >", 10.0f, 1004.0f);
            DrawString(String.Format("   {0}               {1}               {2}", strScore1, strHiScore, strScore2), 10.0f, 970.0f);
            DrawString(String.Format("CREDIT {0}", strCredits), 700.0f, 50.0f);
            GameManager.Draw();   
        }
        public static void DrawString(String pString, float x, float y, ColorName color = ColorName.White)
        {
            Azul.Sprite pAzulSprite = new Azul.Sprite();

            for (int i = 0; i < pString.Length; i++)
            {
                int key = Convert.ToByte(pString[i]);
                Font pFont = FontManager.Find(FontName.Consolas36pt, key);
                Debug.Assert(pFont != null);
                pAzulSprite.Swap(pFont.GetAzulTexture(), pFont.GetAzulRect(),
                                new Azul.Rect(x, y, pFont.GetAzulRect().width, pFont.GetAzulRect().height),
                                ColorFactory.Create(color).pAzulColor);
                pAzulSprite.Update();
                pAzulSprite.Render();
                x += pFont.GetAzulRect().width;
            }
        }
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            Debug.Assert(dLink1 != null);
            Debug.Assert(dLink2 != null);
            Font font1 = (Font)dLink1;
            Font font2 = (Font)dLink2;
            return (font1.key == font2.key && font1.name == font2.name);
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Font();
            Debug.Assert(pNode != null);
            return pNode;
        }

        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            Font pNode = (Font)node;

            Debug.Assert(pNode != null);
            pNode.Dump();
        }
    }
}
