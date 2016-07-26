﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using web;
using Формирование_ЧПУ;

namespace RacerMotors
{
    public partial class Form1 : Form
    {
        web.WebRequest webRequest = new web.WebRequest();
        WebClient webClient = new WebClient();
        FileEdit file = new FileEdit();
        CHPU chpu = new CHPU();
        string otv = null;
        double discount = 0.02;
        int countUpdate = 0;
        int countDelete = 0;

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists("files"))
            {
                Directory.CreateDirectory("files");
            }
            if (!Directory.Exists("pic"))
            {
                Directory.CreateDirectory("pic");
            }

            if (!File.Exists("files\\miniText.txt"))
            {
                File.Create("files\\miniText.txt");
            }

            if (!File.Exists("files\\fullText.txt"))
            {
                File.Create("files\\fullText.txt");
            }

            if (!File.Exists("files\\title.txt"))
            {
                File.Create("files\\title.txt");
            }

            if (!File.Exists("files\\description.txt"))
            {
                File.Create("files\\description.txt");
            }

            if (!File.Exists("files\\keywords.txt"))
            {
                File.Create("files\\keywords.txt");
            }
            StreamReader altText = new StreamReader("files\\miniText.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                rtbMiniText.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\fullText.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                rtbFullText.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\title.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                tbTitle.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\description.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                tbDescription.AppendText(str + "\n");
            }
            altText.Close();

            altText = new StreamReader("files\\keywords.txt", Encoding.GetEncoding("windows-1251"));
            while (!altText.EndOfStream)
            {
                string str = altText.ReadLine();
                tbKeywords.AppendText(str + "\n");
            }
            altText.Close();
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            int count = 0;
            StreamWriter writers = new StreamWriter("files\\miniText.txt", false, Encoding.GetEncoding(1251));
            count = rtbMiniText.Lines.Length;
            for (int i = 0; rtbMiniText.Lines.Length > i; i++)
            {
                if (count - 1 == i)
                {
                    if (rtbFullText.Lines[i] == "")
                        break;
                }
                writers.WriteLine(rtbMiniText.Lines[i].ToString());
            }
            writers.Close();

            writers = new StreamWriter("files\\fullText.txt", false, Encoding.GetEncoding(1251));
            count = rtbFullText.Lines.Length;
            for (int i = 0; count > i; i++)
            {
                if (count - 1 == i)
                {
                    if (rtbFullText.Lines[i] == "")
                        break;
                }
                writers.WriteLine(rtbFullText.Lines[i].ToString());
            }
            writers.Close();

            writers = new StreamWriter("files\\title.txt", false, Encoding.GetEncoding(1251));
            writers.WriteLine(tbTitle.Lines[0]);
            writers.Close();

            writers = new StreamWriter("files\\description.txt", false, Encoding.GetEncoding(1251));
            writers.WriteLine(tbDescription.Lines[0]);
            writers.Close();

            writers = new StreamWriter("files\\keywords.txt", false, Encoding.GetEncoding(1251));
            writers.WriteLine(tbKeywords.Lines[0]);
            writers.Close();

            MessageBox.Show("Сохранено");
        }

        private void btnActualPrice_Click(object sender, EventArgs e)
        {
            File.Delete("naSite.csv");
            File.Delete("allProducts.csv");
            string boldOpen = "<span style=\"\"font-weight: bold; font-weight: bold; \"\">";
            string boldClose = "</span>";
            List<string> newProduct = new List<string>();
            newProduct.Add("id");                                                                               //id
            newProduct.Add("Артикул *");                                                 //артикул
            newProduct.Add("Название товара *");                                          //название
            newProduct.Add("Стоимость товара *");                                    //стоимость
            newProduct.Add("Стоимость со скидкой");                                       //со скидкой
            newProduct.Add("Раздел товара *");                                         //раздел товара
            newProduct.Add("Товар в наличии *");                                                    //в наличии
            newProduct.Add("Поставка под заказ *");                                                 //поставка
            newProduct.Add("Срок поставки (дни) *");                                           //срок поставки
            newProduct.Add("Краткий текст");                                 //краткий текст
            newProduct.Add("Текст полностью");                                          //полностью текст
            newProduct.Add("Заголовок страницы (title)");                               //заголовок страницы
            newProduct.Add("Описание страницы (description)");                                 //описание
            newProduct.Add("Ключевые слова страницы (keywords)");                                 //ключевые слова
            newProduct.Add("ЧПУ страницы (slug)");                                   //ЧПУ
            newProduct.Add("С этим товаром покупают");                              //с этим товаром покупают
            newProduct.Add("Рекламные метки");
            newProduct.Add("Показывать на сайте *");                                           //показывать
            newProduct.Add("Удалить *");                                    //удалить
            file.fileWriterCSV(newProduct, "naSite");

            otv = webRequest.getRequestEncod("http://racer-motors.ru/spare-parts/");
            MatchCollection modelTovar = new Regex("(?<=<li><a href=\")/spare-parts/.*?(?=\">)").Matches(otv);
            for (int i = 0; modelTovar.Count > i; i++)
            {
                bool shlak = modelTovar[i].ToString().Contains("aksessuary");
                if (shlak)
                    break;
                otv = webRequest.getRequestEncod("http://racer-motors.ru" + modelTovar[i].ToString());

                string objProduct = null;
                bool b = modelTovar[i].ToString().Contains("pitbike");
                bool a = modelTovar[i].ToString().Contains("dvigatel");
                if (b)
                {
                    objProduct = "pitbike";
                }
                else if (a)
                {
                    objProduct = "dvigatel";
                }
                else
                {
                    objProduct = new Regex("(?<=/spare-parts/).*?(?=/)").Match(modelTovar[i].ToString()).ToString();
                }

                MatchCollection podrazdel = new Regex("(?<=<li><a href=\")/spare-parts/.*/(?=\">)").Matches(otv);
                string section1 = new Regex("(?<=\"  class=\"sel\">).*?(?=</a></li>)").Match(otv).ToString();
                for (int n = 0; podrazdel.Count > n; n++)
                {
                    otv = webRequest.getRequestEncod("http://racer-motors.ru" + podrazdel[n].ToString());
                    MatchCollection articlRacerMotors = new Regex("(?<=<td >).*?(?=</td>\n.*<td >)").Matches(otv);
                    MatchCollection priceRacerMotors = new Regex("(?<=<td>).*?(?=</td>)").Matches(otv);
                    MatchCollection namesRacerMotors = new Regex("(?<=<div class=\"name_elem\">)[\\w\\W]*?(?=</div>)").Matches(otv);
                    MatchCollection codePicture = new Regex("(?<=<td class=\"code_td\">).*(?=</td>)").Matches(otv);

                    string section2 = new Regex("(?<=<div class=\"name\">)[\\w\\W]*?(?=</div>)").Match(otv).ToString().Trim();
                    string imageProduct = new Regex("(?<=<img src=\").*?(?=\" border=\"0\" alt=\"\" width=\"732\" height=\"383\" />)").Match(otv).ToString();
                    if (articlRacerMotors.Count == priceRacerMotors.Count & namesRacerMotors.Count == priceRacerMotors.Count & articlRacerMotors.Count == namesRacerMotors.Count)
                    {
                        for (int m = 0; articlRacerMotors.Count > m; m++)
                        {
                            try
                            {
                                webClient.DownloadFile("http://racer-motors.ru" + imageProduct, "pic\\" + articlRacerMotors[m].ToString() + ".jpg");
                            }
                            catch
                            {

                            }

                            otv = webRequest.getRequest("http://bike18.ru/products/search/page/1?sort=0&balance=&categoryId=&min_cost=&max_cost=&text=" + articlRacerMotors[m].ToString());
                            string urlTovar = new Regex("(?<=<a href=\").*(?=\"><div class=\"-relative item-image\")").Match(otv).ToString();

                            string nameTovarRacerMotors = namesRacerMotors[m].ToString().Trim();
                            int priceTovarRacerMotorsInt = Convert.ToInt32(priceRacerMotors[m].ToString());
                            double priceTovarRacerMotors = Convert.ToDouble(priceTovarRacerMotorsInt);
                            int priceActual = webRequest.price(priceTovarRacerMotors, discount);

                            if (urlTovar != "")
                            {
                                otv = webRequest.getRequest(urlTovar);
                                string nameTovar = new Regex("(?<=<h1>).*(?=</h1>)").Match(otv).ToString();
                                string priceTovar = new Regex("(?<=<span class=\"product-price-data\" data-cost=\").*?(?=\">)").Match(otv).ToString();

                                if (nameTovarRacerMotors == nameTovar & priceActual.ToString() != priceTovar & priceRacerMotors[m].ToString() != "0")
                                {
                                    urlTovar = urlTovar.Replace("http://bike18.ru/", "http://bike18.nethouse.ru/");
                                    List<string> tovarList = webRequest.arraySaveimage(urlTovar);
                                    tovarList[9] = priceActual.ToString();
                                    webRequest.saveImage(tovarList);
                                    countUpdate++;
                                }

                                if (priceRacerMotors[m].ToString() == "0")
                                {
                                    urlTovar = urlTovar.Replace("http://bike18.ru/", "http://bike18.nethouse.ru/");
                                    List<string> tovarList = webRequest.arraySaveimage(urlTovar);
                                    webRequest.deleteProduct(tovarList);
                                    countDelete++;
                                }
                            }
                            else
                            {
                                if (priceActual != 0)
                                {
                                    string slug = chpu.vozvr(nameTovarRacerMotors);

                                    string razdel = "Запчасти и расходники => Каталог запчастей RACER => Запчасти на ";
                                    string minitext = null;
                                    string titleText = null;
                                    string descriptionText = null;
                                    string keywordsText = null;
                                    string fullText = null;
                                    string dblProdSEO = null;

                                    string dblProduct = "НАЗВАНИЕ также подходит для: аналогичных моделей.";

                                    switch (objProduct)
                                    {
                                        case ("motorcycles"):
                                            razdel = razdel + "Мотоцикл " + section1;
                                            break;
                                        case ("scooters"):
                                            razdel = razdel + "Скутер " + section1;
                                            break;
                                        case ("mopeds"):
                                            razdel = razdel + "Мопед " + section1;
                                            break;
                                        case ("pitbike"):
                                            razdel = razdel + "Питбайки " + section1;
                                            break;
                                        default:
                                            razdel = razdel + " " + section1;
                                            break;
                                    }
                                    string nameText = boldOpen + nameTovarRacerMotors + boldClose;
                                    string nameRazdel = boldOpen + section1 + boldClose;
                                    string namePodrazdel = boldOpen + section2 + boldClose;

                                    for (int z = 0; rtbMiniText.Lines.Length > z; z++)
                                    {
                                        if (rtbMiniText.Lines[z].ToString() == "")
                                        {
                                            minitext += "<p><br /></p>";
                                        }
                                        else
                                        {
                                            minitext += "<p>" + rtbMiniText.Lines[z].ToString() + "</p>";
                                        }
                                    }

                                    for (int z = 0; rtbFullText.Lines.Length > z; z++)
                                    {
                                        if (rtbFullText.Lines[z].ToString() == "")
                                        {
                                            fullText += "<p><br /></p>";
                                        }
                                        else
                                        {
                                            fullText += "<p>" + rtbFullText.Lines[z].ToString() + "</p>";
                                        }
                                    }

                                    titleText = tbTitle.Lines[0].ToString();
                                    descriptionText = tbDescription.Lines[0].ToString() + " " + dblProdSEO;
                                    keywordsText = tbKeywords.Lines[0].ToString();

                                    string strCodePage = "Номер " + codePicture[m].ToString() + " на схеме/фото";
                                    strCodePage = boldOpen + strCodePage + boldClose;

                                    string discount = "<p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> Сделай ТРОЙНОЙ удар по нашим ценам! </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 1. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Скидки за отзывы о товарах!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 2. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Друзьям скидки и подарки!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 3. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Нашли дешевле!? 110% разницы Ваши!</a></span></p>";

                                    minitext = minitext.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", namePodrazdel).Replace("РАЗДЕЛ", nameRazdel).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameText).Replace("АРТИКУЛ", articlRacerMotors[m].ToString()).Replace("<p><br /></p><p><br /></p><p><br /></p><p>", "<p><br /></p>");

                                    minitext = minitext.Remove(minitext.LastIndexOf("<p>"));

                                    fullText = fullText.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", namePodrazdel).Replace("РАЗДЕЛ", nameRazdel).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameText).Replace("АРТИКУЛ", articlRacerMotors[m].ToString());

                                    fullText = fullText.Remove(fullText.LastIndexOf("<p>"));

                                    titleText = titleText.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", section2).Replace("РАЗДЕЛ", section1).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameTovarRacerMotors).Replace("АРТИКУЛ", articlRacerMotors[m].ToString());

                                    descriptionText = descriptionText.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", section2).Replace("РАЗДЕЛ", section1).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameTovarRacerMotors).Replace("АРТИКУЛ", articlRacerMotors[m].ToString());


                                    keywordsText = keywordsText.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", section2).Replace("РАЗДЕЛ", section1).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameTovarRacerMotors).Replace("АРТИКУЛ", articlRacerMotors[m].ToString());

                                    if (titleText.Length > 255)
                                    {
                                        titleText = titleText.Remove(255);
                                        titleText = titleText.Remove(titleText.LastIndexOf(" "));
                                    }
                                    if (descriptionText.Length > 200)
                                    {
                                        descriptionText = descriptionText.Remove(200);
                                        descriptionText = descriptionText.Remove(descriptionText.LastIndexOf(" "));
                                    }
                                    if (keywordsText.Length > 100)
                                    {
                                        keywordsText = keywordsText.Remove(100);
                                        keywordsText = keywordsText.Remove(keywordsText.LastIndexOf(" "));
                                    }
                                    if (slug.Length > 64)
                                    {
                                        slug = slug.Remove(64);
                                        slug = slug.Remove(slug.LastIndexOf(" "));
                                    }

                                    newProduct = new List<string>();
                                    newProduct.Add(""); //id
                                    newProduct.Add("\"" + articlRacerMotors[m].ToString() + "\""); //артикул
                                    newProduct.Add("\"" + nameTovarRacerMotors + "\"");  //название
                                    newProduct.Add("\"" + priceActual + "\""); //стоимость
                                    newProduct.Add("\"" + "" + "\""); //со скидкой
                                    newProduct.Add("\"" + razdel + "\""); //раздел товара
                                    newProduct.Add("\"" + "100" + "\""); //в наличии
                                    newProduct.Add("\"" + "0" + "\"");//поставка
                                    newProduct.Add("\"" + "1" + "\"");//срок поставки
                                    newProduct.Add("\"" + minitext + "\"");//краткий текст
                                    newProduct.Add("\"" + fullText + "\"");//полностью текст
                                    newProduct.Add("\"" + titleText + "\""); //заголовок страницы
                                    newProduct.Add("\"" + descriptionText + "\""); //описание
                                    newProduct.Add("\"" + keywordsText + "\"");//ключевые слова
                                    newProduct.Add("\"" + slug + "\""); //ЧПУ
                                    newProduct.Add(""); //с этим товаром покупают
                                    newProduct.Add("");   //рекламные метки
                                    newProduct.Add("\"" + "1" + "\"");  //показывать
                                    newProduct.Add("\"" + "0" + "\""); //удалить

                                    file.fileWriterCSV(newProduct, "naSite");
                                }
                            }
                            List<string> allProducts = new List<string>();
                            allProducts.Add(nameTovarRacerMotors);
                            allProducts.Add(articlRacerMotors[m].ToString());
                            allProducts.Add(section1);
                            allProducts.Add(section2);
                            file.fileWriterCSV(allProducts, "allProducts");
                        }
                    }
                }
            }
            string[] allProductsLines = File.ReadAllLines("allProducts.csv", Encoding.GetEncoding(1251));
            string[] noProducts = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
            if (noProducts.Length != 1)
            {
                File.Delete("naSite.csv");
                List<string> newPro = new List<string>();
                string str0 = noProducts[0].ToString();
                newPro.Add(str0);
                for (int i = 1; noProducts.Length > i; i++)
                {
                    string dblProduct = "";
                    string[] newProducts = noProducts[i].Split(';');
                    string articlNewProduct = newProducts[1].Replace("\"", "");
                    string nameNewProduct = newProducts[2].Replace("\"", "");
                    foreach (string str in allProductsLines)
                    {
                        string[] allProducts = str.Split(';');
                        string allProductsArticl = allProducts[1].Replace("\"", "");
                        if (articlNewProduct == allProductsArticl)
                        {
                            dblProduct += "<br />-" + boldOpen + allProducts[2].Replace("\"", "") + boldClose + " раздел " + boldOpen + allProducts[3].Replace("\"", "") + boldClose;
                        }
                    }
                    dblProduct = dblProduct + "<br />и аналогичных моделей.";
                    string neww = noProducts[i].Replace("аналогичных моделей.", dblProduct);
                    newPro.Add(neww);
                    File.WriteAllLines("naSite.csv", newPro, Encoding.GetEncoding(1251));
                }
            }

            System.Threading.Thread.Sleep(20000);
            string trueOtv = null;
            //cookie = webRequest.webCookieBike18();
            string[] naSite1 = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
            if (naSite1.Length > 1)
            {
                do
                {
                    string otvimg = DownloadNaSite();
                    string check = "{\"success\":true,\"imports\":{\"state\":1,\"errorCode\":0,\"errorLine\":0}}";
                    do
                    {
                        System.Threading.Thread.Sleep(2000);
                        otvimg = ChekedLoading();
                    }
                    while (otvimg == check);

                    trueOtv = new Regex("(?<=\":{\"state\":).*?(?=,\")").Match(otvimg).ToString();
                    string error = new Regex("(?<=errorCode\":).*?(?=,\")").Match(otvimg).ToString();
                    if (error == "13")
                    {
                        string errstr = new Regex("(?<=errorLine\":).*?(?=,\")").Match(otvimg).ToString();
                        string[] naSite = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
                        int u = Convert.ToInt32(errstr) - 1;
                        MatchCollection strslug3 = new Regex("(?<=\\\";\\\").*?(?=\\\")").Matches(naSite[u]);
                        string strslug = strslug3[12].ToString();
                        //методом подбора ищется ЧПУ
                        int slug = strslug.Length;
                        string strslug2 = strslug.Remove(slug - 2);
                        strslug2 += "1";
                        naSite[u] = naSite[u].Replace(strslug, strslug2);
                        File.WriteAllLines("naSite.csv", naSite, Encoding.GetEncoding(1251));
                    }
                    if (error == "37")
                    {
                        string errstr = new Regex("(?<=errorLine\":).*?(?=,\")").Match(otvimg).ToString();
                        string[] naSite = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
                        int u = Convert.ToInt32(errstr) - 1;
                        MatchCollection strslug3 = new Regex("(?<=\\\";\\\").*?(?=\\\")").Matches(naSite[u]);
                        string strslug = strslug3[12].ToString();
                        //методом подбора ищется ЧПУ
                        int slug = strslug.Length;
                        string strslug2 = strslug.Remove(slug - 2);
                        strslug2 += "1";
                        naSite[u] = naSite[u].Replace(strslug, strslug2);
                        File.WriteAllLines("naSite.csv", naSite, Encoding.GetEncoding(1251));
                    }
                }
                while (trueOtv != "2");
            }
            MessageBox.Show("Обновлено товаров на сайте: " + countUpdate + "\nУдалено товаров с сайта: " + countDelete);
        }

        public string DownloadNaSite()
        {
            CookieContainer cookie = webRequest.webCookieBike18();
            string epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString().Replace(",", "");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://bike18.nethouse.ru/api/export-import/import-from-csv?fileapi" + epoch);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=---------------------------12709277337355";
            req.CookieContainer = cookie;
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            byte[] csv = File.ReadAllBytes("naSite.csv");
            byte[] end = Encoding.ASCII.GetBytes("\r\n-----------------------------12709277337355\r\nContent-Disposition: form-data; name=\"_catalog_file\"\r\n\r\nnaSite.csv\r\n-----------------------------12709277337355--\r\n");
            byte[] ms1 = Encoding.ASCII.GetBytes("-----------------------------12709277337355\r\nContent-Disposition: form-data; name=\"catalog_file\"; filename=\"naSite.csv\"\r\nContent-Type: text/csv\r\n\r\n");
            req.ContentLength = ms1.Length + csv.Length + end.Length;
            Stream stre1 = req.GetRequestStream();
            stre1.Write(ms1, 0, ms1.Length);
            stre1.Write(csv, 0, csv.Length);
            stre1.Write(end, 0, end.Length);
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            string otvimg = ressrImg.ReadToEnd();
            return otvimg;
        }

        public string ChekedLoading()
        {
            CookieContainer cookie = webRequest.webCookieBike18();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://bike18.nethouse.ru/api/export-import/check-import");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentLength = 0;
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            Stream stre1 = req.GetRequestStream();
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            string otvimg = ressrImg.ReadToEnd();
            return otvimg;
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo("Запчасти.xlsx");
            ExcelPackage p = new ExcelPackage(file);
            ExcelWorksheet w = p.Workbook.Worksheets[1];
            int q = w.Dimension.Rows;
            for(int i = 14; q > i; i++)
            {
                if(w.Cells[i, 3].Value == null)
                {
                    string razdel = (string)w.Cells[i, 2].Value;
                }
                else
                {
                    string name = (string)w.Cells[i, 2].Value;
                    string articl = (string)w.Cells[i, 3].Value;
                    string nomenclatura = (string)w.Cells[i, 5].Value;
                }
            }
        }
    }
}
