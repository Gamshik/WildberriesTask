using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;


using HttpClient client = new HttpClient();

var uri = "https://cart-storage-api.wildberries.ru/api/basket/sync?ts=1711491775719&device_id=site_a5c8bb9c64fd462cb302c769126dcec8";

using var request = new HttpRequestMessage(HttpMethod.Post, uri);

request.Headers.Add("Accept", "*/*");
request.Headers.Add("Accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTE0Nzk5NTQsInZlcnNpb24iOjIsInVzZXIiOiIxNDkyODQ2NjkiLCJzaGFyZF9rZXkiOiIyMyIsImNsaWVudF9pZCI6IndiIiwic2Vzc2lvbl9pZCI6IjRlM2I0ZjRlNTk3NDQyYWFhNzRmN2IwODA5NGE4YjBkIiwidXNlcl9yZWdpc3RyYXRpb25fZHQiOjE3MTE0NTIxMjksInZhbGlkYXRpb25fa2V5IjoiNGY5NWY3N2FhZWJhODFiZjQxN2EwOGVhNzI0NGU4N2YxZDMwNThhMmExZTVlNzA3NWE5YTljMjcxNDI2ODViMSIsInBob25lIjoiemNNcmdXenJraU5RQUZUbEpjallhQT09In0.E4XCp0LfhDDzL1rlXdmd8k1z8BbZJoNXJEUiQUCvGaJQ47mWgxTYkM8yWqWId0n9A8Y79aTq_NxUkiWPeZGLjnwMKM6vN9aWU_puP35360CuBXZOQJHVO17OBHjpKgh1vHNH1OQg8o1o5wrzmC3KzwRaXa7pFSk4fgWatUv5sSoXVZ_rGrQyWwQwkylqqa24qZOP3E5sEHLy5zqVgGF3Z20fHmjOGoQiko8lXzBvbzg6D0fdzorIWgy-6RNhsdFNDCjsZUNMafxBg5qN_s6H1mx3Mlp5jFFIjTsHNocuS7X9teMBOw7NrRiyNWe2gZBD7HglLGCLmS8KeW5VD707dw");
request.Headers.Add("Origin", "https://www.wildberries.ru");
request.Headers.Add("Referer", "https://www.wildberries.ru/catalog/35650072/detail.aspx");
request.Headers.Add("Sec-ch-ua", "\"Google Chrome\";v=\"123\", \"Not:A-Brand\";v=\"8\", \"Chromium\";v=\"123\"");
request.Headers.Add("Sec-ch-ua-mobile", "?0");
request.Headers.Add("Sec-ch-ua-platform", "\"Windows\"");
request.Headers.Add("Sec-fetch-dest", "empty");
request.Headers.Add("Sec-fetch-mode", "cors");
request.Headers.Add("Sec-fetch-site", "same-site");
request.Headers.Add("User-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");

string content = "[{\"chrt_id\": 205206107,\"quantity\": 10,\"cod_1s\": 186896828,\"client_ts\": 1711489991,\"op_type\": 1,\"target_url\": \"EX|2|MCS|IT|popular|||||\"}]";

request.Content = new StringContent(content, Encoding.UTF8, "application/json");
using var response = await client.SendAsync(request);
var responseText = await response.Content.ReadAsStringAsync();
Console.WriteLine(response.StatusCode);