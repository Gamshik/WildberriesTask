using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;


namespace WildberriesTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Корзина до добавления товара: ");

        /// <summary>
        /// Метод получающий информацию о карточках находящихся в корзине
        /// </summary>
        /// <returns>Информация о карточках находящихся в корзине в формате JSON</returns>
        private static async Task<object> GetBasketAsync()
        {
            using HttpClient client = new HttpClient();

            var uri = "https://cart-storage-api.wildberries.ru/api/basket/sync?ts=1711572511992&device_id=site_a5c8bb9c64fd462cb302c769126dcec8";

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            // тип принимаемого контента
            request.Headers.Add("Accept", "*/*");
            // допустимые языки для ответа
            request.Headers.Add("Accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            // jwt токен
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTE0Nzk5NTQsInZlcnNpb24iOjIsInVzZXIiOiIxNDkyODQ2NjkiLCJzaGFyZF9rZXkiOiIyMyIsImNsaWVudF9pZCI6IndiIiwic2Vzc2lvbl9pZCI6IjRlM2I0ZjRlNTk3NDQyYWFhNzRmN2IwODA5NGE4YjBkIiwidXNlcl9yZWdpc3RyYXRpb25fZHQiOjE3MTE0NTIxMjksInZhbGlkYXRpb25fa2V5IjoiNGY5NWY3N2FhZWJhODFiZjQxN2EwOGVhNzI0NGU4N2YxZDMwNThhMmExZTVlNzA3NWE5YTljMjcxNDI2ODViMSIsInBob25lIjoiemNNcmdXenJraU5RQUZUbEpjallhQT09In0.E4XCp0LfhDDzL1rlXdmd8k1z8BbZJoNXJEUiQUCvGaJQ47mWgxTYkM8yWqWId0n9A8Y79aTq_NxUkiWPeZGLjnwMKM6vN9aWU_puP35360CuBXZOQJHVO17OBHjpKgh1vHNH1OQg8o1o5wrzmC3KzwRaXa7pFSk4fgWatUv5sSoXVZ_rGrQyWwQwkylqqa24qZOP3E5sEHLy5zqVgGF3Z20fHmjOGoQiko8lXzBvbzg6D0fdzorIWgy-6RNhsdFNDCjsZUNMafxBg5qN_s6H1mx3Mlp5jFFIjTsHNocuS7X9teMBOw7NrRiyNWe2gZBD7HglLGCLmS8KeW5VD707dw");
            // домен
            request.Headers.Add("Origin", "https://www.wildberries.ru");
            // источник запроса
            request.Headers.Add("Referer", "https://www.wildberries.ru/lk/basket");
            // информация о клиенте
            request.Headers.Add("Sec-ch-ua", "\"Google Chrome\";v=\"123\", \"Not:A-Brand\";v=\"8\", \"Chromium\";v=\"123\"");
            request.Headers.Add("Sec-ch-ua-mobile", "?0");
            request.Headers.Add("Sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("Sec-fetch-dest", "empty");
            request.Headers.Add("Sec-fetch-mode", "cors");
            request.Headers.Add("Sec-fetch-site", "same-site");
            request.Headers.Add("User-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");

            request.Content = new StringContent("[]", Encoding.UTF8, "application/json");

            using var response = await client.SendAsync(request);

            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(responseText);
        }
        /// <summary>
        /// Метод получающий параметр товара optionId
        /// </summary>
        /// <param name="nomenclature">Номенклатура товара</param>
        /// <returns>Значение параметра товара optionId</returns>
        /// <exception cref="ArgumentException"></exception>
        private static async Task<string> GetOptionIdAsync(string nomenclature)
        {
            using HttpClient client = new HttpClient();

            var url = $"https://card.wb.ru/cards/v2/detail?appType=1&curr=rub&dest=-1257786&spp=30&nm={nomenclature}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "*/*");

            using var response = await client.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            Regex regex = new Regex(@"""optionId"":(?<optionId>\d+),");
            Match match = regex.Match(responseText);

            if (!match.Success)
                throw new ArgumentException("Неверная номенклатура!");

            string optionId = match.Groups["optionId"].Value;

            return optionId;
        }
        /// <summary>
        /// Метод создающий заказ на товар в корзине личного кабинета Wildberries
        /// </summary>
        /// <param name="nomenclature">Номенклатура</param>
        /// <param name="optionId">OptionId</param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> CardInBasketAsync(string nomenclature, string optionId)
        {
            using HttpClient client = new HttpClient();

            var uri = "https://cart-storage-api.wildberries.ru/api/basket/sync?ts=1711570395313&device_id=site_a5c8bb9c64fd462cb302c769126dcec8";

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            // тип принимаемого контента
            request.Headers.Add("Accept", "*/*");
            // допустимые языки для ответа
            request.Headers.Add("Accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            // jwt токен
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTE0Nzk5NTQsInZlcnNpb24iOjIsInVzZXIiOiIxNDkyODQ2NjkiLCJzaGFyZF9rZXkiOiIyMyIsImNsaWVudF9pZCI6IndiIiwic2Vzc2lvbl9pZCI6IjRlM2I0ZjRlNTk3NDQyYWFhNzRmN2IwODA5NGE4YjBkIiwidXNlcl9yZWdpc3RyYXRpb25fZHQiOjE3MTE0NTIxMjksInZhbGlkYXRpb25fa2V5IjoiNGY5NWY3N2FhZWJhODFiZjQxN2EwOGVhNzI0NGU4N2YxZDMwNThhMmExZTVlNzA3NWE5YTljMjcxNDI2ODViMSIsInBob25lIjoiemNNcmdXenJraU5RQUZUbEpjallhQT09In0.E4XCp0LfhDDzL1rlXdmd8k1z8BbZJoNXJEUiQUCvGaJQ47mWgxTYkM8yWqWId0n9A8Y79aTq_NxUkiWPeZGLjnwMKM6vN9aWU_puP35360CuBXZOQJHVO17OBHjpKgh1vHNH1OQg8o1o5wrzmC3KzwRaXa7pFSk4fgWatUv5sSoXVZ_rGrQyWwQwkylqqa24qZOP3E5sEHLy5zqVgGF3Z20fHmjOGoQiko8lXzBvbzg6D0fdzorIWgy-6RNhsdFNDCjsZUNMafxBg5qN_s6H1mx3Mlp5jFFIjTsHNocuS7X9teMBOw7NrRiyNWe2gZBD7HglLGCLmS8KeW5VD707dw");
            // домен
            request.Headers.Add("Origin", "https://www.wildberries.ru");
            // источник запроса
            request.Headers.Add("Referer", "https://www.wildberries.ru/lk");
            // информация о клиенте
            request.Headers.Add("Sec-ch-ua", "\"Google Chrome\";v=\"123\", \"Not:A-Brand\";v=\"8\", \"Chromium\";v=\"123\"");
            request.Headers.Add("Sec-ch-ua-mobile", "?0");
            request.Headers.Add("Sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("Sec-fetch-dest", "empty");
            request.Headers.Add("Sec-fetch-mode", "cors");
            request.Headers.Add("Sec-fetch-site", "same-site");
            request.Headers.Add("User-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
            // chrt_id - option_id,
            // cod_1s - номенклатура,
            // client - время добавления в корзину,
            // target_utl - откуда на карточку пришел пользователь, 
            string content = $"[{{\"chrt_id\": {optionId},\"quantity\": 1,\"cod_1s\": {nomenclature},\"client_ts\": 1711570739,\"op_type\": 1,\"target_url\": \"EX|2|MCS|IT|popular|||||\"}}]";

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = await client.SendAsync(request);
            Console.WriteLine("Статус код запроса на добавление карточки в корзину - " + response.StatusCode);

            return response;
        }
        /// <summary>
        /// Метод получающий строку введённую пользователем
        /// </summary>
        /// <returns>Строка введённая пользователем</returns>
        private static string GetString()
        {
            string @string;
            while (string.IsNullOrEmpty(@string = Console.ReadLine()))
            {
                Console.WriteLine("Вы ввели некорректную строку! Попробуйте ещё раз:");
            }

            return @string;
        }
    }
}