using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Threading;
using System.Drawing;

namespace AutoBrowseChrome
{
    class Program
    {
        static void Main(string[] args)
        {
            //string uri = "https://ramapi.kidshelpphone.ca/api/v2/resource/favour/json/9BB1408A-81DD-43D1-B621-83E2F71D2669/en/Both/18122169/19/8";
            ////uri = "http://google.com";
            //string proxyip = "38.64.129.242:8080";

            int proxytime = AutoBrowseEdge.Properties.Settings.Default.proxytime;
            int times = AutoBrowseEdge.Properties.Settings.Default.times;

            string web = AutoBrowseEdge.Properties.Settings.Default.page;
            string cate = AutoBrowseEdge.Properties.Settings.Default.cate;
            string proxies = AutoBrowseEdge.Properties.Settings.Default.proxies;
            string psearch = AutoBrowseEdge.Properties.Settings.Default.psearch;
            string[] allweb = File.ReadAllLines(web);
            string[] allCate = File.ReadAllLines(cate);
            string[] allproxy = File.ReadAllLines(proxies);
            string[] allsearch = File.ReadAllLines(psearch);

            Random rnd1 = new Random();

            string thisPage = "";
            string thisCate = "";
            string thissearch = "";
            string PROXY = "";

            Proxy proxy = new Proxy();
            EdgeOptions options = new EdgeOptions();

            TimeSpan timespan = new TimeSpan();
            timespan = TimeSpan.FromMinutes(1);

            string path = Directory.GetCurrentDirectory();



            while (true)
            {
                Thread.Sleep(proxytime * 1000);
                {
                    // random get a proxy server IP and port#     proxy_ip_# like "38.64.129.242:8080";
                    PROXY = allproxy[rnd1.Next(allproxy.Length)];
                    proxy.SslProxy = PROXY;
                    proxy.SocksProxy = PROXY;
                    proxy.HttpProxy = PROXY;

                    options.Proxy = proxy;


                    //each proxy browe 2 categories and 3 webpages 1 search

                    // look at a category 
                    {
                        //thisCate = allCate[rnd1.Next(allCate.Length)];
                        thisPage = allweb[rnd1.Next(allCate.Length)];
                        EdgeDriver driver = new EdgeDriver(path, options, timespan);
                        driver.Manage().Window.Size = new Size(300, 200);
                        driver.Navigate().GoToUrl(thisCate);  //   Category 如果要扫目录就改成 thisCate
                        driver.Close();
                    }


                    ///  open a search
                    Thread.Sleep(1000 * AutoBrowseEdge.Properties.Settings.Default.Iterval1); // then look at a page
                    {
                        thissearch = allsearch[rnd1.Next(allsearch.Length)];
                        EdgeDriver driver = new EdgeDriver(path, options, timespan);
                        driver.Manage().Window.Size = new Size(300, 200);
                        driver.Navigate().GoToUrl(thissearch); //   Search  如果要扫目录就改成 thisCate
                        driver.Close();
                    }

                    // open a page
                    Thread.Sleep(1000 * AutoBrowseEdge.Properties.Settings.Default.Iterval1); // then look at a page
                    {
                        thisPage = allweb[rnd1.Next(allweb.Length)];
                        EdgeDriver driver = new EdgeDriver(path, options, timespan);
                        driver.Manage().Window.Size = new Size(300, 200);
                        driver.Navigate().GoToUrl(thisPage); //   Page 如果要扫目录就改成 thisCate
                        driver.Close();
                    }

                    // look at a category 
                    Thread.Sleep(1000 * AutoBrowseEdge.Properties.Settings.Default.Iterval2); // and then back to a category
                    {
                        thisCate = allCate[rnd1.Next(allCate.Length)];
                        EdgeDriver driver = new EdgeDriver(path, options, timespan);
                        driver.Manage().Window.Size = new Size(300, 200);
                        driver.Navigate().GoToUrl(thisCate);//   Category 如果要扫目录就改成 thisCate
                        driver.Close();
                    } 


                    // open a page
                    Thread.Sleep(1000 * AutoBrowseEdge.Properties.Settings.Default.Iterval3);// and then back to a category again 
                    {
                        //thisCate = allCate[rnd1.Next(allCate.Length)];
                        thisPage = allweb[rnd1.Next(allweb.Length)];
                        EdgeDriver driver = new EdgeDriver(path, options, timespan);
                        driver.Manage().Window.Size = new Size(300, 200);
                        driver.Navigate().GoToUrl(thisPage);//   Page 如果要扫目录就改成 thisCate
                        driver.Close();
                    }


                    // open a page
                    Thread.Sleep(1000 * AutoBrowseEdge.Properties.Settings.Default.Iterval4); // finally look at one more page
                    {
                        thisPage = allweb[rnd1.Next(allweb.Length)];
                        EdgeDriver driver = new EdgeDriver(path, options, timespan);
                        driver.Manage().Window.Size = new Size(300, 200);
                        driver.Navigate().GoToUrl(thisPage);//   Page 如果要扫目录就改成 thisCate
                        driver.Close();
                    }
                }
            }
        }
    }
}
