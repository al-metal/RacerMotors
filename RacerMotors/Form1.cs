﻿using Bike18;
using OfficeOpenXml;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using web;
using Формирование_ЧПУ;

namespace RacerMotors
{
    public partial class Form1 : Form
    {
        Thread forms;

        web.WebRequest webRequest = new web.WebRequest();
        nethouse nethouse = new nethouse();
        WebClient webClient = new WebClient();
        httpRequest httprequest = new httpRequest();

        CHPU chpu = new CHPU();
        string boldOpen = "<span style=\"\"font-weight: bold; font-weight: bold; \"\">";
        string boldClose = "</span>";
        string otv = null;
        int addCount = 0;
        double discounts = 0.02;
        int countUpdate = 0;
        int countDelete = 0;

        string minitextTemplate;
        string fullTextTemplate;
        string keywordsTextTemplate;
        string titleTextTemplate;
        string descriptionTextTemplate;
        string fileUrls;

        FileEdit files = new FileEdit();

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

        #region Нажатие кнопок
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
            Properties.Settings.Default.login = tbLogin.Text;
            Properties.Settings.Default.password = tbPasswords.Text;
            Properties.Settings.Default.Save();

            minitextTemplate = MinitextStr();
            fullTextTemplate = FulltextStr();
            keywordsTextTemplate = tbKeywords.Lines[0].ToString();
            titleTextTemplate = tbTitle.Lines[0].ToString();
            descriptionTextTemplate = tbDescription.Lines[0].ToString();

            Thread tabl = new Thread(() => UpdateTovar());
            forms = tabl;
            forms.IsBackground = true;
            forms.Start();
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.login = tbLogin.Text;
            Properties.Settings.Default.password = tbPasswords.Text;
            Properties.Settings.Default.Save();

            fileUrls = "";
            ofdLoadPrice.ShowDialog();
            
            fileUrls = ofdLoadPrice.FileName.ToString();

            if (ofdLoadPrice.FileName == "openFileDialog1" || ofdLoadPrice.FileName == "")
            {
                MessageBox.Show("Ошибка при выборе файла", "Ошибка файла");
                return;
            }

            minitextTemplate = MinitextStr();
            fullTextTemplate = FulltextStr();
            keywordsTextTemplate = tbKeywords.Lines[0].ToString();
            titleTextTemplate = tbTitle.Lines[0].ToString();
            descriptionTextTemplate = tbDescription.Lines[0].ToString();

            Thread tabl = new Thread(() => UpdateTovarXLSX());
            forms = tabl;
            forms.IsBackground = true;
            forms.Start();

        }

        private void btnUpdateImg_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.login = tbLogin.Text;
            Properties.Settings.Default.password = tbPasswords.Text;
            Properties.Settings.Default.Save();

            Thread tabl = new Thread(() => UpdateImages());
            forms = tabl;
            forms.IsBackground = true;
            forms.Start();

        }
        #endregion

        private void UpdateImages()
        {
            CookieContainer cookie = nethouse.CookieNethouse(tbLogin.Text, tbPasswords.Text);
            if (cookie.Count == 1)
            {
                MessageBox.Show("Логин или пароль для сайта введены не верно", "Ошибка логина/пароля");
                return;
            }

            ControlsFormEnabledFalse();

            int countUpdateImage = 0;
            otv = webRequest.getRequest("http://bike18.ru/products/category/1185370");
            MatchCollection razdel = new Regex("(?<=</div></a><div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);

            lblNamePosition.Invoke(new Action(() => lblNamePosition.Text = "Раздел"));
            lblVsegoRazdelov.Invoke(new Action(() => lblVsegoRazdelov.Text = razdel.Count.ToString()));
            for (int i = 0; razdel.Count > i; i++)
            {
                lblRazdel.Invoke(new Action(() => lblRazdel.Text = (i + 1).ToString()));
                otv = webRequest.getRequest("http://bike18.ru" + razdel[i].ToString() + "/page/all");
                MatchCollection tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*(?=\" >)").Matches(otv);
                for (int n = 0; tovar.Count > n; n++)
                {
                    bool b = false;
                    string articl = null;
                    string images = null;
                    string alsoby = null;
                    string productGroupe = null;

                    List<string> listProd = nethouse.GetProductList(cookie, tovar[n].ToString());
                    articl = listProd[6];
                    images = listProd[32];
                    alsoby = listProd[42];
                    productGroupe = listProd[3];

                    if (images == "")
                    {
                        if (File.Exists("Pic\\" + articl + ".jpg"))
                        {
                            nethouse.UploadImage(cookie, tovar[n].ToString());
                            b = true;
                            countUpdateImage++;
                        }
                    }

                    if (alsoby == "&alsoBuy[0]=0")
                    {
                        listProd[42] = nethouse.alsoBuyTovars(listProd);
                        b = true;
                        countUpdateImage++;
                    }

                    if (productGroupe != "10833347")
                    {
                        listProd[3] = "10833347";
                        b = true;
                        countUpdateImage++;
                    }

                    if (b)
                        nethouse.SaveTovar(cookie, listProd);
                }
            }

            ControlsFormEnabledTrue();

            MessageBox.Show("Было изменено: " + countUpdateImage + " товаров");
        }

        private void ControlsFormEnabledTrue()
        {
            btnActualPrice.Invoke(new Action(() => btnActualPrice.Enabled = true));
            btnPrice.Invoke(new Action(() => btnPrice.Enabled = true));
            btnSaveTemplate.Invoke(new Action(() => btnSaveTemplate.Enabled = true));
            btnUpdateImg.Invoke(new Action(() => btnUpdateImg.Enabled = true));
            rtbFullText.Invoke(new Action(() => rtbFullText.Enabled = true));
            rtbMiniText.Invoke(new Action(() => rtbMiniText.Enabled = true));
            tbDescription.Invoke(new Action(() => tbDescription.Enabled = true));
            tbKeywords.Invoke(new Action(() => tbKeywords.Enabled = true));
            tbLogin.Invoke(new Action(() => tbLogin.Enabled = true));
            tbPasswords.Invoke(new Action(() => tbPasswords.Enabled = true));
            tbTitle.Invoke(new Action(() => tbTitle.Enabled = true));
        }

        private void ControlsFormEnabledFalse()
        {
            btnActualPrice.Invoke(new Action(() => btnActualPrice.Enabled = false));
            btnPrice.Invoke(new Action(() => btnPrice.Enabled = false));
            btnSaveTemplate.Invoke(new Action(() => btnSaveTemplate.Enabled = false));
            btnUpdateImg.Invoke(new Action(() => btnUpdateImg.Enabled = false));
            rtbFullText.Invoke(new Action(() => rtbFullText.Enabled = false));
            rtbMiniText.Invoke(new Action(() => rtbMiniText.Enabled = false));
            tbDescription.Invoke(new Action(() => tbDescription.Enabled = false));
            tbKeywords.Invoke(new Action(() => tbKeywords.Enabled = false));
            tbLogin.Invoke(new Action(() => tbLogin.Enabled = false));
            tbPasswords.Invoke(new Action(() => tbPasswords.Enabled = false));
            tbTitle.Invoke(new Action(() => tbTitle.Enabled = false));
        }

        private void UpdateTovarXLSX()
        {
            CookieContainer cookie = nethouse.CookieNethouse(tbLogin.Text, tbPasswords.Text);
            if (cookie.Count == 1)
            {
                MessageBox.Show("Логин или пароль для сайта введены не верно", "Ошибка логина/пароля");
                return;
            }

            ControlsFormEnabledFalse();

            int countEditPrice = 0;
            int countAddCSV = 0;
            File.Delete("naSite.csv");
            List<string> newProduct = newList();
            bool b = false;
            string razdelCSV = null;

            FileInfo file = new FileInfo(fileUrls);
            ExcelPackage p = new ExcelPackage(file);
            ExcelWorksheet w = p.Workbook.Worksheets[1];
            int q = w.Dimension.Rows;

            lblVsegoRazdelov.Invoke(new Action(() => lblVsegoRazdelov.Text = (q - 14).ToString()));
            lblNamePosition.Invoke(new Action(() => lblNamePosition.Text = "Товар"));

            for (int i = 14; q > i; i++)
            {
                if (w.Cells[i, 3].Value == null)
                {
                    razdelCSV = (string)w.Cells[i, 4].Value;
                    razdelCSV = razdelCSV.Trim();
                    if (razdelCSV != "Метизы" & razdelCSV != "Универсальные запчасти")
                        razdelCSV = returnRazdel(razdelCSV);
                    if (razdelCSV == "Универсальные запчасти")
                        b = true;
                    else
                        b = false;
                }
                else
                {
                    lblRazdel.Invoke(new Action(() => lblRazdel.Text = (i - 14).ToString()));
                    if (razdelCSV != "Метизы")
                    {
                        string name = (string)w.Cells[i, 4].Value;
                        name = name.Trim();
                        string articl = (string)w.Cells[i, 2].Value.ToString();
                        string nomenclatura = "";
                        try
                        {
                            nomenclatura = (string)w.Cells[i, 3].Value;
                        }
                        catch
                        {
                            nomenclatura = "";
                        }
                        double priceCSV = (double)w.Cells[i, 10].Value;
                        string dopnomenrlatura = (string)w.Cells[i, 5].Value;
                        if (dopnomenrlatura != null)
                            dopnomenrlatura = dopnomenrlatura.Replace("\"", "");

                        otv = webRequest.getRequest("http://bike18.ru/products/search/page/1?sort=0&balance=&categoryId=&min_cost=&max_cost=&text=+" + articl);
                        string urlTovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Match(otv).ToString();

                        if (urlTovar != "")
                        {
                            string priceTovar = new Regex("(?<=<span class=\"product-price-data\" data-cost=\").*?(?=\">)").Match(otv).ToString();
                            double actualPrice = webRequest.price(priceCSV, discounts);

                            if (actualPrice != Convert.ToDouble(priceTovar))
                            {
                                urlTovar = urlTovar.Replace("http://bike18.ru/", "http://bike18.nethouse.ru/");
                                List<string> tovar = nethouse.GetProductList(cookie, urlTovar);
                                tovar[9] = actualPrice.ToString();
                                nethouse.SaveTovar(cookie, tovar);
                                countEditPrice++;
                            }
                        }
                        else
                        {
                            string razdelchik = "";
                            if (razdelCSV == "Универсальные запчасти")
                            {
                                otv = webRequest.getRequestEncod("http://racer-motors.ru/search/index.php?q=" + nomenclatura + "&s=");
                                string newSearch = new Regex("(?<=В запросе \"<a href=\").*(?=\" onclick=\")").Match(otv).ToString();
                                razdelchik = new Regex("(?<=<br /><hr />)[\\w\\W]*(?=<p></p>)").Match(otv).ToString();
                                if (razdelchik != "")
                                {
                                    razdelchik = new Regex("(?<=<a href=\").*(?=\">)").Match(razdelchik).ToString();
                                    otv = webRequest.getRequestEncod("http://racer-motors.ru" + razdelchik);
                                    MatchCollection articlesNames = new Regex("(?<=<td >).*?(?=</td>)").Matches(otv);
                                    foreach (Match str in articlesNames)
                                    {
                                        if (str.ToString() == articl)
                                        {
                                            razdelchik = new Regex("(?<=<h1> <span class=\"name_model\">).*?(?=</span></h1>)").Match(otv).ToString().Replace(",", "").Replace("! ", "").Replace("Racer ", "");
                                            razdelCSV = returnRazdel(razdelchik);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    string urlProductSearch = "";
                                    if (newSearch != "")
                                    {
                                        otv = webRequest.getRequestEncod("http://racer-motors.ru" + newSearch);
                                        urlProductSearch = new Regex("(?<=<a href=\").*(?=\"><b>)").Match(otv).ToString();
                                    }
                                    else
                                    {
                                        urlProductSearch = new Regex("(?<=<br /><hr />)[\\w\\W]*(?=<p></p>)").Match(otv).ToString();
                                        urlProductSearch = new Regex("(?<=<a href=\").*(?=\">)").Match(urlProductSearch).ToString();
                                    }

                                    if (urlProductSearch != "")
                                    {
                                        otv = webRequest.getRequestEncod("http://racer-motors.ru" + urlProductSearch);
                                        razdelchik = new Regex("(?<=<h1> <span class=\"name_model\">).*?(?=</span></h1>)").Match(otv).ToString().Replace(",", "");
                                    }
                                }
                            }
                            if (razdelCSV.Contains("Разное"))
                            {
                                otv = webRequest.getRequestEncod("http://racer-motors.ru/search/index.php?q=" + nomenclatura + "&s=");
                                string newSearch = new Regex("(?<=В запросе \"<a href=\").*(?=\" onclick=\")").Match(otv).ToString();
                                razdelchik = new Regex("(?<=<a href=\").*(?=\"><b>)").Match(otv).ToString();
                                if (razdelchik != "")
                                {
                                    otv = webRequest.getRequestEncod("http://racer-motors.ru" + razdelchik);
                                    razdelchik = new Regex("(?<=<h1> <span class=\"name_model\">).*?(?=</span></h1>)").Match(otv).ToString().Replace(",", "").Replace("! ", "");
                                }
                                else
                                {
                                    string urlProductSearch = "";
                                    if (newSearch != "")
                                    {
                                        otv = webRequest.getRequestEncod("http://racer-motors.ru" + newSearch);
                                        urlProductSearch = new Regex("(?<=<a href=\").*(?=\"><b>)").Match(otv).ToString();
                                    }
                                    else
                                    {
                                        urlProductSearch = new Regex("(?<=<br /><hr />)[\\w\\W]*(?=<p></p>)").Match(otv).ToString();
                                        urlProductSearch = new Regex("(?<=<a href=\").*(?=\">)").Match(urlProductSearch).ToString();
                                    }

                                    if (urlProductSearch != "")
                                    {
                                        otv = webRequest.getRequestEncod("http://racer-motors.ru" + urlProductSearch);
                                        razdelchik = new Regex("(?<=<h1> <span class=\"name_model\">).*?(?=</span></h1>)").Match(otv).ToString().Replace(",", "");
                                    }
                                }
                                razdelCSV = returnRazdel(razdelchik);
                            }
                            if (razdelCSV == "Универсальные запчасти")
                                razdelCSV = "Разное";
                            string razdel = "Запчасти и расходники => Каталог запчастей RACER => ";
                            razdel = razdel + razdelCSV;

                            //Добавляем на сайт
                            string slug = chpu.vozvr(name);
                            double actualPrice = webRequest.price(priceCSV, discounts);

                            string dblProdSEO = null;
                            string dblProduct = "НАЗВАНИЕ также подходит для:<br />" + boldOpen + dopnomenrlatura + boldClose + " аналогичных моделей.";

                            string minitext = minitextTemplate;
                            string titleText = titleTextTemplate;
                            string descriptionText = descriptionTextTemplate + " " + dblProdSEO;
                            string keywordsText = keywordsTextTemplate;
                            string fullText = fullTextTemplate;
                            string nameText = boldOpen + name + boldClose;
                            string discount = Discount();

                            minitext = minitext.Replace("СКИДКА", discount).Replace("РАЗДЕЛ", dopnomenrlatura).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameText).Replace("АРТИКУЛ", articl).Replace("<p><br /></p><p><br /></p><p><br /></p><p>", "<p><br /></p>").Replace("<p>НОМЕРФОТО</p>", "");

                            minitext = minitext.Remove(minitext.LastIndexOf("<p>"));

                            fullText = fullText.Replace("СКИДКА", discount).Replace("РАЗДЕЛ", dopnomenrlatura).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameText).Replace("АРТИКУЛ", articl);

                            fullText = fullText.Remove(fullText.LastIndexOf("<p>"));

                            titleText = titleText.Replace("СКИДКА", discount).Replace("РАЗДЕЛ", razdelCSV).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", name).Replace("АРТИКУЛ", articl);

                            descriptionText = descriptionText.Replace("СКИДКА", discount).Replace("РАЗДЕЛ", razdelCSV).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", name).Replace("АРТИКУЛ", articl);

                            keywordsText = keywordsText.Replace("СКИДКА", discount).Replace("РАЗДЕЛ", razdelCSV).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", name).Replace("АРТИКУЛ", articl);

                            titleText = Remove(titleText, 255);
                            descriptionText = Remove(descriptionText, 200);
                            keywordsText = Remove(keywordsText, 100);
                            slug = Remove(slug, 64);

                            newProduct = new List<string>();
                            newProduct.Add(""); //id
                            newProduct.Add("\"" + articl + "\""); //артикул
                            newProduct.Add("\"" + name + "\"");  //название
                            newProduct.Add("\"" + actualPrice + "\""); //стоимость
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
                            if (razdelCSV != "Метизы")
                            {
                                files.fileWriterCSV(newProduct, "naSite");
                                countAddCSV++;
                            }
                            if (b)
                                razdelCSV = "Универсальные запчасти";
                        }
                    }
                }
            }
            System.Threading.Thread.Sleep(20000);
            string[] naSite1 = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
            if (naSite1.Length > 1)
                nethouse.UploadCSVNethouse(cookie, "naSite.csv");

            MessageBox.Show("Обновлено цен: " + countEditPrice + "\nДобавлено позиций: " + countAddCSV);

            ControlsFormEnabledTrue();
        }

        private void UpdateTovar()
        {

            CookieContainer cookie = nethouse.CookieNethouse(tbLogin.Text, tbPasswords.Text);
            if (cookie.Count == 1)
            {
                MessageBox.Show("Логин или пароль для сайта введены не верно", "Ошибка логина/пароля");
                return;
            }

            ControlsFormEnabledFalse();

            File.Delete("naSite.csv");
            File.Delete("allProducts.csv");
            List<string> newProduct = newList();

            otv = webRequest.getRequestEncod("http://racer-motors.ru/spare-parts/");
            MatchCollection modelTovar = new Regex("(?<=<li><a href=\")/spare-parts/.*?(?=\">)").Matches(otv);
            lblNamePosition.Invoke(new Action(() => lblNamePosition.Text = "Раздел"));
            lblVsegoRazdelov.Invoke(new Action(() => lblVsegoRazdelov.Text = modelTovar.Count.ToString()));
            for (int i = 0; modelTovar.Count > i; i++)
            {
                lblRazdel.Invoke(new Action(() => lblRazdel.Text = (i + 1).ToString()));
                string objProduct = null;
                bool shlak = false;
                shlak = modelTovar[i].ToString().Contains("aksessuary");

                if (!shlak)
                {
                    otv = webRequest.getRequestEncod("http://racer-motors.ru" + modelTovar[i].ToString());

                    bool b = modelTovar[i].ToString().Contains("pitbike");
                    bool a = modelTovar[i].ToString().Contains("dvigatel");

                    if (b)
                        objProduct = "pitbike";
                    else if (a)
                        objProduct = "dvigatel";
                    else
                        objProduct = new Regex("(?<=/spare-parts/).*?(?=/)").Match(modelTovar[i].ToString()).ToString();

                    string section1 = new Regex("(?<=\"  class=\"sel\">).*?(?=</a></li>)").Match(otv).ToString();
                    section1 = section1.Replace(",", "");
                    MatchCollection podrazdel = new Regex("(?<=<li><a href=\")/spare-parts/.*/(?=\">)").Matches(otv);
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
                                string nameTovarRacerMotors = namesRacerMotors[m].ToString().Trim();
                                int priceTovarRacerMotorsInt = Convert.ToInt32(priceRacerMotors[m].ToString());
                                double priceTovarRacerMotors = Convert.ToDouble(priceTovarRacerMotorsInt);
                                int priceActual = webRequest.price(priceTovarRacerMotors, discounts);
                                string articlRacer = articlRacerMotors[m].ToString();

                                DownloadImages("http://racer-motors.ru" + imageProduct, articlRacerMotors[m].ToString());

                                string urlTovar = nethouse.searchTovar(nameTovarRacerMotors, nameTovarRacerMotors);
                                if (urlTovar == null)
                                    urlTovar = nethouse.searchTovar(nameTovarRacerMotors, articlRacer);

                                if (urlTovar != null)
                                {
                                    #region Обновление данных товара
                                    List<string> tovar = nethouse.GetProductList(cookie, urlTovar);
                                    string nameTovarBike = tovar[4].ToString();
                                    string preciTovarBike = tovar[9].ToString();
                                    bool izmen = false;

                                    if (nameTovarRacerMotors == nameTovarBike & priceActual.ToString() != preciTovarBike & priceRacerMotors[m].ToString() != "0")
                                    {
                                        tovar[9] = priceActual.ToString();
                                        izmen = true;
                                    }

                                    if (priceRacerMotors[m].ToString() == "0")
                                    {
                                        tovar[43] = "100";
                                        izmen = true;
                                    }

                                    if (tovar[39] != "")
                                    {
                                        tovar[39] = "";
                                        izmen = true;
                                    }

                                    if (izmen)
                                    {
                                        tovar[39] = "";
                                        nethouse.SaveTovar(cookie, tovar);
                                        countUpdate++;
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region Запись товара в файл для загрузки
                                    bool t = false;
                                    string[] tovars = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
                                    foreach (string str in tovars)
                                    {
                                        string articl = articlRacerMotors[m].ToString();
                                        string[] strTovar = str.Split(';');
                                        if (strTovar[1] == "\"" + articl + "\"")
                                            t = true;
                                    }

                                    if (priceActual != 0 & !t)
                                    {
                                        string dblProdSEO = null;
                                        string dblProduct = "НАЗВАНИЕ также подходит для: аналогичных моделей.";
                                        string slug = chpu.vozvr(nameTovarRacerMotors);
                                        string razdel = Razdel(objProduct, section1);
                                        string minitext = minitextTemplate;
                                        string titleText = titleTextTemplate;
                                        string descriptionText = descriptionTextTemplate + " " + dblProdSEO;
                                        string keywordsText = keywordsTextTemplate;
                                        string fullText = fullTextTemplate;
                                        string article = articlRacerMotors[m].ToString();

                                        string strCodePage = boldOpen + "Номер " + codePicture[m].ToString() + " на схеме/фото" + boldClose;

                                        minitext = Replace(minitext, section2, section1, strCodePage, dblProduct, nameTovarRacerMotors, article);
                                        minitext = minitext.Remove(minitext.LastIndexOf("<p>"));

                                        fullText = Replace(fullText, section2, section1, strCodePage, dblProduct, nameTovarRacerMotors, article);
                                        fullText = fullText.Remove(fullText.LastIndexOf("<p>"));

                                        titleText = ReplaceSEO(titleText, nameTovarRacerMotors, section1, section2, article, dblProduct, strCodePage);
                                        descriptionText = ReplaceSEO(descriptionText, nameTovarRacerMotors, section1, section2, article, dblProduct, strCodePage);
                                        keywordsText = ReplaceSEO(keywordsText, nameTovarRacerMotors, section1, section2, article, dblProduct, strCodePage);

                                        titleText = Remove(titleText, 255);
                                        descriptionText = Remove(descriptionText, 200);
                                        keywordsText = Remove(keywordsText, 100);
                                        slug = Remove(slug, 64);

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

                                        files.fileWriterCSV(newProduct, "naSite");
                                    }
                                }
                                #endregion

                                List<string> allProducts = new List<string>();
                                allProducts.Add(nameTovarRacerMotors);
                                allProducts.Add(articlRacerMotors[m].ToString());
                                allProducts.Add(section1);
                                allProducts.Add(section2);
                                files.fileWriterCSV(allProducts, "allProducts");
                            }
                        }
                    }
                }
            }

            #region Аналогичные товары в краткое описание товара
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
            #endregion

            System.Threading.Thread.Sleep(20000);
            string[] naSite1 = File.ReadAllLines("naSite.csv", Encoding.GetEncoding(1251));
            if (naSite1.Length > 1)
                nethouse.UploadCSVNethouse(cookie, "naSite.csv");

            MessageBox.Show("Обновлено товаров на сайте: " + countUpdate + "\nУдалено товаров с сайта: " + countDelete);

            ControlsFormEnabledTrue();
        }

        private int ReturnCountAdd()
        {
            if (addCount == 99)
                addCount = 0;
            addCount++;
            return addCount;
        }

        private void DownloadImages(string urlImg, object article)
        {
            if (!File.Exists("Pic\\" + article + ".jpg"))
            {
                try
                {
                    webClient.DownloadFile(urlImg, "Pic\\" + article + ".jpg");
                }
                catch
                {

                }
            }
        }

        private string Razdel(string objProduct, string section1)
        {
            string razdel = "Запчасти и расходники => Каталог запчастей RACER => Запчасти на ";
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
                    razdel = razdel + section1;
                    break;
            }
            return razdel;
        }

        private string MinitextStr()
        {
            string minitext = "";
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
            return minitext;
        }

        private string FulltextStr()
        {
            string fullText = "";
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
            return fullText;
        }

        private string Replace(string text, string section2, string section1, string strCodePage, string dblProduct, string nameTovarRacerMotors, string article)
        {
            string discount = Discount();
            string nameText = boldOpen + nameTovarRacerMotors + boldClose;
            string nameRazdel = boldOpen + section1 + boldClose;
            string namePodrazdel = boldOpen + section2 + boldClose;
            text = text.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", namePodrazdel).Replace("РАЗДЕЛ", nameRazdel).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameText).Replace("АРТИКУЛ", article).Replace("<p><br /></p><p><br /></p><p><br /></p><p>", "<p><br /></p>");
            return text;
        }

        private string ReplaceSEO(string text, string nameTovarRacerMotors, string section1, string section2, string article, string dblProduct, string strCodePage)
        {
            string discount = Discount();
            text = text.Replace("СКИДКА", discount).Replace("ПОДРАЗДЕЛ", section2).Replace("РАЗДЕЛ", section1).Replace("НОМЕРФОТО", strCodePage).Replace("ДУБЛЬ", dblProduct).Replace("НАЗВАНИЕ", nameTovarRacerMotors).Replace("АРТИКУЛ", article);
            return text;
        }

        private string Discount()
        {
            string discount = "<p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> Сделай ТРОЙНОЙ удар по нашим ценам! </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 1. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Скидки за отзывы о товарах!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 2. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Друзьям скидки и подарки!</a> </span></p><p style=\"\"text-align: right;\"\"><span style=\"\"font -weight: bold; font-weight: bold;\"\"> 3. <a target=\"\"_blank\"\" href =\"\"http://bike18.ru/stock\"\"> Нашли дешевле!? 110% разницы Ваши!</a></span></p>";
            return discount;
        }

        private string Remove(string text, int v)
        {
            if (text.Length > v)
            {
                text = text.Remove(v);
                text = text.Remove(text.LastIndexOf(" "));
            }
            return text;
        }

        private List<string> newList()
        {
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
            files.fileWriterCSV(newProduct, "naSite");
            return newProduct;
        }

        private string returnRazdel(string razdelCSV)
        {
            razdelCSV = razdelCSV.Replace("Racer ", "");
            switch (razdelCSV)
            {
                case "Двигатель 139QMB 50 cc":
                    razdelCSV = "Запчасти на Двигатель 139QMB 50 cm3";
                    break;
                case "Двигатель 147FMD 70 cc":
                    razdelCSV = "Запчасти на Двигатель 147FMD 70 cm3";
                    break;
                case "Двигатель 152QMI 125 cc":
                    razdelCSV = "Запчасти на Двигатель 157QMJ 150 cm3 152QMI 125 cm3";
                    break;
                case "Двигатель 153FMI 125 cc":
                    razdelCSV = "Запчасти на Двигатель 153FMI 125 cm3 (RC125-PE)";
                    break;
                case "Двигатель 154FMI 130 cc":
                    razdelCSV = "Запчасти на Двигатель 154FMI 130 cm3 (RC130CF)";
                    break;
                case "Двигатель 157QMJ 150 cc":
                    razdelCSV = "Запчасти на Двигатель 157QMJ 150 cm3 152QMI 125 cm3";
                    break;
                case "Двигатель 161FMJ 150 cc (RC150-23)":
                    razdelCSV = "Запчасти на Двигатель 161FMJ 150 cm3 (RC150-23)";
                    break;
                case "Двигатель 161FMJ 150 cc (RC150-GY)":
                    razdelCSV = "Запчасти на Двигатель 161FMJ 150 cm3 (RC150-GY)";
                    break;
                case "Двигатель 163FML 200 cc":
                    razdelCSV = "Запчасти на Двигатель 163FML 200 cm3 (RC200XZT)";
                    break;
                case "Двигатель 164FML 200 cc":
                    razdelCSV = "Запчасти на Двигатель 164FML 200 cc (RC200CK RC200CS RC200-C5B RC200GY-C2 RC200-GY8) 166FMM 250 cc (RC250CK RC250CK-N RC250CS RC250-C5B RC250GY-C2)";
                    break;
                case "Двигатель 164FML 200 cc (RC200ZH)":
                    razdelCSV = "Запчасти на Двигатель 164FML 200 cm3 (RC200ZH)";
                    break;
                case "Двигатель 153FMH 110 cm3":
                    razdelCSV = "Запчасти на Двигатель 153FMH 110 cm3";
                    break;
                case "Двигатель 153FMH 110 cc":
                    razdelCSV = "Запчасти на Двигатель 153FMH 110 cm3";
                    break;
                case "Двигатель 165FML 200 cc":
                    razdelCSV = "Запчасти на Двигатель 165FML 200 cm3 (RC200LT)";
                    break;
                case "Двигатель 166FMM 250 cc":
                    razdelCSV = "Запчасти на Двигатель 164FML 200 cc (RC200CK RC200CS RC200-C5B RC200GY-C2 RC200-GY8) 166FMM 250 cc (RC250CK RC250CK-N RC250CS RC250-C5B RC250GY-C2)";
                    break;
                case "Двигатель 170FMN 300 cc":
                    razdelCSV = "Запчасти на Двигатель 170FMN 300 cm3 (RC300-GY8 RC300CS)";
                    break;
                case "Двигатель 1P39FMA 50 cc":
                    razdelCSV = "Запчасти на Двигатель 1P39FMA 50 cm3";
                    break;
                case "Двигатель 1P52FMH 110 cc":
                    razdelCSV = "Запчасти на Двигатель 1P52FMH 110 cm3";
                    break;
                case "Двигатель 1P54FMI 125 сс":
                    razdelCSV = "Запчасти на двигатель 1P54FMI 125 cm3 (RC125-PM)";
                    break;
                case "Двигатель 1P60FMK 160 сс":
                    razdelCSV = "Запчасти на Двигатель 1P60FMK 160 cm3 (RC160-PM RC160-PH)";
                    break;
                case "Двигатель 257FMM 250 cc":
                    razdelCSV = "Запчасти на Двигатель 257FMM 250 cm3 (RC250LV)";
                    break;
                case "Двигатель ZS177MM 250 cc":
                    razdelCSV = "Запчасти на Двигатель ZS177MM 250 cm3 (RC250XZR RC250-GY8)";
                    break;
                case "Мопед CM50Q-2 Delta":
                    razdelCSV = "Запчасти на Мопед Racer CM50Q-2 Delta";
                    break;
                case "Мопед RC125T-9X Flame":
                    razdelCSV = "Запчасти на Скутер Racer RC125T-9X Flame";
                    break;
                case "Мопед RC50QT-15 Stells":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-15 Stells";
                    break;
                case "Мопед RC50QT-15J Taurus":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-15J Taurus";
                    break;
                case "Мопед RC50QT-19 Arrow":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-19 Arrow";
                    break;
                case "Мопед RC50QT-3 Meteor":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-3 Meteor";
                    break;
                case "Мопед RC50QT-6 Sagita":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-6 Sagita";
                    break;
                case "Мопед RC50QT-9/RC125T-9 Lupus":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-9 Lupus";
                    break;
                case "Мопед RC50QT-9V Corvus":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-9V Corvus";
                    break;
                case "Мотоцикл CM110 Indigo":
                    razdelCSV = "Запчасти на Мопед Racer CM110 Indigo";
                    break;
                case "Мотоцикл RC110N Trophy":
                    razdelCSV = "Запчасти на Мопед Racer RC110N Trophy";
                    break;
                case "Мотоцикл RC125, RC160 Pitbike":
                    razdelCSV = "Запчасти на Питбайки Racer RC125-PE RC125-PM RC160-PM RC160-PH Pitbike";
                    break;
                case "Мотоцикл RC130CF Viper":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC130CF Viper";
                    break;
                case "Мотоцикл RC150-10D Triumph":
                    razdelCSV = "Разное";
                    break;
                case "Мотоцикл RC150-23 Tiger":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC150-23 Tiger";
                    break;
                case "Мотоцикл RC150-GY Enduro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC150-GY Enduro";
                    break;
                case "Мотоцикл RC150T-11 Dragon":
                    razdelCSV = "Запчасти на Скутер Racer RC150T-11 Dragon";
                    break;
                case "Мотоцикл RC200-C5B Magnum":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200-C5B RC250-C5B Magnum";
                    break;
                case "Мотоцикл RC200-CS Skyway":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200-CS Skyway";
                    break;
                case "Мотоцикл RC200-GY8 Ranger":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200-GY8 Ranger";
                    break;
                case "Мотоцикл RC200CK/RC250CK Nitro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200CK/RC250CK Nitro";
                    break;
                case "Мотоцикл RC200GY-C2 Panther":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200GY-C2 RC250GY-C2 Panther";
                    break;
                case "Мотоцикл RC200LT Forester":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200LT Forester";
                    break;
                case "Мотоцикл RC200XZT Enduro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200XZT Enduro";
                    break;
                case "Мотоцикл RC200ZH Muravei":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200ZH Muravei";
                    break;
                case "Мотоцикл RC250-GY8 Crossrunner":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250-GY8 Crossrunner";
                    break;
                case "Мотоцикл RC250CK-N Fighter":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250CK-N Fighter";
                    break;
                case "Мотоцикл RC250CS/RC300CS Skyway":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250CS Skyway";
                    break;
                case "Мотоцикл RC250LV Cruiser":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250LV Cruiser";
                    break;
                case "Мотоцикл RC250NC-X1 Phantom":
                    razdelCSV = "Разное";
                    break;
                case "Мотоцикл RC250XZR Enduro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250XZR Enduro";
                    break;
                case "Мотоцикл RC300-GY8 Ranger":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC300-GY8 Ranger";
                    break;
                case "Мотоцикл RC50/CM70 Alpha":
                    razdelCSV = "Запчасти на Мопед Racer RC70 Alpha";
                    break;
                case "Мотоцикл RC200GY-C2/RC250GY-C2 Panther":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200GY-C2 RC250GY-C2 Panther";
                    break;


                //----------------------------------------------------




                case "139QMB 50 cc":
                    razdelCSV = "Запчасти на Двигатель 139QMB 50 cm3";
                    break;
                case "147FMD 70 cc":
                    razdelCSV = "Запчасти на Двигатель 147FMD 70 cm3";
                    break;
                case "152QMI 125 cc":
                    razdelCSV = "Запчасти на Двигатель 157QMJ 150 cm3 152QMI 125 cm3";
                    break;
                case "153FMI 125 cc":
                    razdelCSV = "Запчасти на Двигатель 153FMI 125 cm3 (RC125-PE)";
                    break;
                case "154FMI 130 cc":
                    razdelCSV = "Запчасти на Двигатель 154FMI 130 cm3 (RC130CF)";
                    break;
                case "157QMJ 150 cc":
                    razdelCSV = "Запчасти на Двигатель 157QMJ 150 cm3 152QMI 125 cm3";
                    break;
                case "161FMJ 150 cc (RC150-23)":
                    razdelCSV = "Запчасти на Двигатель 161FMJ 150 cm3 (RC150-23)";
                    break;
                case "161FMJ 150 cc (RC150-GY)":
                    razdelCSV = "Запчасти на Двигатель 161FMJ 150 cm3 (RC150-GY)";
                    break;
                case "163FML 200 cc":
                    razdelCSV = "Запчасти на Двигатель 163FML 200 cm3 (RC200XZT)";
                    break;
                case "164FML 200 cc":
                    razdelCSV = "Запчасти на Двигатель 164FML 200 cc (RC200CK RC200CS RC200-C5B RC200GY-C2 RC200-GY8) 166FMM 250 cc (RC250CK RC250CK-N RC250CS RC250-C5B RC250GY-C2)";
                    break;
                case "164FML 200 cc (RC200ZH)":
                    razdelCSV = "Запчасти на Двигатель 164FML 200 cm3 (RC200ZH)";
                    break;
                case "153FMH 110 cm3":
                    razdelCSV = "Запчасти на Двигатель 153FMH 110 cm3";
                    break;
                case "165FML 200 cc":
                    razdelCSV = "Запчасти на Двигатель 165FML 200 cm3 (RC200LT)";
                    break;
                case "166FMM 250 cc":
                    razdelCSV = "Запчасти на Двигатель 164FML 200 cc (RC200CK RC200CS RC200-C5B RC200GY-C2 RC200-GY8) 166FMM 250 cc (RC250CK RC250CK-N RC250CS RC250-C5B RC250GY-C2)";
                    break;
                case "170FMN 300 cc":
                    razdelCSV = "Запчасти на Двигатель 170FMN 300 cm3 (RC300-GY8 RC300CS)";
                    break;
                case "1P39FMA 50 cc":
                    razdelCSV = "Запчасти на Двигатель 1P39FMA 50 cm3";
                    break;
                case "1P52FMH 110 cc":
                    razdelCSV = "Запчасти на Двигатель 1P52FMH 110 cm3";
                    break;
                case "1P54FMI 125 сс":
                    razdelCSV = "Запчасти на двигатель 1P54FMI 125 cm3 (RC125-PM)";
                    break;
                case "1P60FMK 160 сс":
                    razdelCSV = "Запчасти на Двигатель 1P60FMK 160 cm3 (RC160-PM RC160-PH)";
                    break;
                case "257FMM 250 cc":
                    razdelCSV = "Запчасти на Двигатель 257FMM 250 cm3 (RC250LV)";
                    break;
                case "ZS177MM 250 cc":
                    razdelCSV = "Запчасти на Двигатель ZS177MM 250 cm3 (RC250XZR RC250-GY8)";
                    break;
                case "CM50Q-2 Delta":
                    razdelCSV = "Запчасти на Мопед Racer CM50Q-2 Delta";
                    break;
                case "RC125T-9X Flame":
                    razdelCSV = "Запчасти на Скутер Racer RC125T-9X Flame";
                    break;
                case "RC50QT-15 Stells":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-15 Stells";
                    break;
                case "RC50QT-15J Taurus":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-15J Taurus";
                    break;
                case "RC50QT-19 Arrow":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-19 Arrow";
                    break;
                case "RC50QT-3 Meteor":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-3 Meteor";
                    break;
                case "RC50QT-6 Sagita":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-6 Sagita";
                    break;
                case "RC50QT-9/RC125T-9 Lupus":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-9 Lupus";
                    break;
                case "RC50QT-9V Corvus":
                    razdelCSV = "Запчасти на Скутер Racer RC50QT-9V Corvus";
                    break;
                case "CM110 Indigo":
                    razdelCSV = "Запчасти на Мопед Racer CM110 Indigo";
                    break;
                case "RC110N Trophy":
                    razdelCSV = "Запчасти на Мопед Racer RC110N Trophy";
                    break;
                case "RC125, RC160 Pitbike":
                    razdelCSV = "Запчасти на Питбайки Racer RC125-PE RC125-PM RC160-PM RC160-PH Pitbike";
                    break;
                case "RC130CF Viper":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC130CF Viper";
                    break;
                case "RC150-10D Triumph":
                    razdelCSV = "Разное";
                    break;
                case "RC150-23 Tiger":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC150-23 Tiger";
                    break;
                case "RC150-GY Enduro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC150-GY Enduro";
                    break;
                case "RC150T-11 Dragon":
                    razdelCSV = "Запчасти на Скутер Racer RC150T-11 Dragon";
                    break;
                case "RC200-C5B Magnum":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200-C5B RC250-C5B Magnum";
                    break;
                case "RC200-CS Skyway":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200-CS Skyway";
                    break;
                case "RC200-GY8 Ranger":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200-GY8 Ranger";
                    break;
                case "RC200CK/RC250CK Nitro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200CK/RC250CK Nitro";
                    break;
                case "RC200GY-C2 Panther":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200GY-C2 RC250GY-C2 Panther";
                    break;
                case "RC200LT Forester":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200LT Forester";
                    break;
                case "RC200XZT Enduro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200XZT Enduro";
                    break;
                case "RC200ZH Muravei":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200ZH Muravei";
                    break;
                case "RC250-GY8 Crossrunner":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250-GY8 Crossrunner";
                    break;
                case "RC250CK-N Fighter":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250CK-N Fighter";
                    break;
                case "RC250CS/RC300CS Skyway":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250CS Skyway";
                    break;
                case "RC250CS Skyway":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250CS Skyway";
                    break;
                case "RC250LV Cruiser":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250LV Cruiser";
                    break;
                case "RC125T-19 Arrow":
                    razdelCSV = "Запчасти на Скутер Racer RC125T-19 Arrow";
                    break;
                case "RC250NC-X1 Phantom":
                    razdelCSV = "Разное";
                    break;
                case "RC250XZR Enduro":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC250XZR Enduro";
                    break;
                case "RC300-GY8 Ranger":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC300-GY8 Ranger";
                    break;
                case "RC50/CM70 Alpha":
                    razdelCSV = "Запчасти на Мопед Racer RC70 Alpha";
                    break;
                case "RC200GY-C2 RC250GY-C2 Panther":
                    razdelCSV = "Запчасти на Мотоцикл Racer RC200GY-C2 RC250GY-C2 Panther";
                    break;
                case "Универсальные запчасти":
                    razdelCSV = "Разное";
                    break;
                default:
                    razdelCSV = "Разное";
                    break;
            }
            return razdelCSV;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbLogin.Text = Properties.Settings.Default.login;
            tbPasswords.Text = Properties.Settings.Default.password;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная программа служит для получения товара\nс сайта www.racer-motors.ru\nа так же обработки прайса данного сайта\n\n\nПрограмма разработана: al-metal\nemail: al-metal@bk.ru", "О программе",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
