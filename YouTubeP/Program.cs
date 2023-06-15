
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using YouTubeP;
using System.Data.SqlClient;
namespace SeleniumYouTube
{
    public class YouTube
    {

        public static void Main()
        {

            var options = new ChromeOptions();

            options.BinaryLocation = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";

            var driver = new ChromeDriver("C:\\Users\v.kovalenko\\Desktop\\Тестовое\\YouTubeP\\chromedriver_win32", options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(@"https://www.youtube.com/results?search_query=%D0%B0%D0%B2%D1%82%D0%BE+%D1%80%D0%B5%D1%82%D1%80%D0%BE&sp=EgIQAg%253D%253D");



            By elem_channel = By.CssSelector("#content-section");
            ReadOnlyCollection<IWebElement> channels = driver.FindElements(elem_channel);
            Console.WriteLine("Общее количество каналов " + channels.Count);
            var links = driver.FindElements(By.CssSelector("#avatar-section > a"));




            foreach (IWebElement link in links)
            {


                Console.WriteLine("{0} - {1}", link.Text, link.GetAttribute("href"));


                string str_views;
                IWebElement elem_video_views = driver.FindElement(By.XPath("(//span[contains(@id,'video-count')])[1]"));
                str_views = elem_video_views.Text;

                String name = driver.FindElement(By.XPath("(//yt-formatted-string[contains(@class,'style-scope ytd-channel-name')])[1]")).Text;
                String subscribers = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-channel-renderer[1]/div/div[2]/a/div[1]/div/span[3]")).Text;

                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//a[contains(.,'подписчиков')]")).Click();
                Thread.Sleep(1000);
                string Url = driver.Url;
                driver.FindElement(By.XPath("(//div[contains(.,'О канале')])[10]")).Click();
                Thread.Sleep(1000);


                string TotalViews = driver.FindElement(By.CssSelector("#right-column > yt-formatted-string:nth-child(3)")).Text;
                Console.WriteLine("Общее количество  просмотров  " + TotalViews);

                string DateReg = driver.FindElement(By.CssSelector("#right-column > yt-formatted-string:nth-child(2) > span:nth-child(2)")).Text;
                Console.WriteLine("Дата регистрации:  " + DateReg);

                driver.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");

                Thread.Sleep(1000);



                string description = "";

                foreach (var item in driver.FindElements(By.Id("description")))
                {
                    string info = item.GetAttribute("innerText");
                    if (info != null)
                    {
                        if (info.Length > 0)
                        {
                            description = info;
                        }
                    }
                }

                Thread.Sleep(1000);

                string UrlWithEmail = "";
                string ss = driver.FindElement(By.CssSelector("a.yt-simple-endpoint.yt-formatted-string")).Text;
                if (ss.Length > 1)
                {
                    UrlWithEmail = Url;
                    Console.WriteLine("Cодержит email " + UrlWithEmail);
                }
                else
                {
                    Console.WriteLine("Не содержит email ");
                }



                Console.WriteLine("Название description = " + description);
                Console.WriteLine("Название канала = " + name);

                Console.WriteLine("Кол-во подписчиков" + subscribers);
                //Console.WriteLine("кол-во видео на канале " + str_views);
                Console.WriteLine("url " + Url);

                Thread.Sleep(1000);
                driver.Navigate().Back();
                driver.Navigate().Back();




                string connection2 = "Server=DESKTOP-VITALII ;Initial Catalog=DataBase;Integrated Security=True ";
                using (SqlConnection connection3 = new SqlConnection(connection2))
                {
                    connection3.Open();
                    string add_sql = "INSERT INTO ParserYouTube VALUES(@Name,@Url,@TotalViews,@Subscribers,@DateReg,@Description,@UrlWithEmail)";

                    using (SqlCommand command = new SqlCommand(add_sql, connection3))

                    {

                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Url", Url);
                        command.Parameters.AddWithValue("@TotalViews", TotalViews);
                        command.Parameters.AddWithValue("@Subscribers", subscribers);
                        command.Parameters.AddWithValue("@DateReg", DateReg);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@UrlWithEmail", UrlWithEmail);
                        int number = command.ExecuteNonQuery();
                        Console.WriteLine("Добавлено объектов: {0}", number);
                    }

                }

            }
        }

    }
}





