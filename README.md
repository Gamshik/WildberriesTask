# Wildberries task

## Задание

Требуется написать код, который создает заказ в корзине личного кабинета Wildberries.

Технические пояснения:

- номенклатура берется из ссылки на  карточку, например
https://www.wildberries.ru/catalog/148748593/detail.aspx - номенклатура 148748593;
- для записи в корзину должны использоваться  Rest API запросы в ЛК Wildberries;
- результатом работы программы должна служить строка в корзине с заданной номенклатурой (передается как параметр);
- код должен быть написан на языке C# .NET Core. Вызовы API проводится стандартно через Http Client;
- формат приложения - любой, проще всего консольное приложение, но можно и Unit tests.
  
## Пояснения

Перед запуском приложения нужно изменить следующие значения:

1) в данном URL адресе изменить значение параметра *device_id* на своё. Данное значение можно посмотреть в *Local storage* по ключу *wbx__sessionID*;
```c#
var url = "https://cart-storage-api.wildberries.ru/api/basket/sync?ts=1711572511992&device_id=site_a5c8bb9c64fd462cb302c769126dcec8";
```

2) заменить JWT token на свой. Его можно посмотреть в *Local storage* по ключу *wbx__tokenData*;
```c#
const string JwtToken = "значение_токена";
```

3) желательно заменить значение следующих заголовков на свои.
```c#
request.Headers.Add("Sec-ch-ua", "\"Google Chrome\";v=\"123\", \"Not:A-Brand\";v=\"8\", \"Chromium\";v=\"123\"");
request.Headers.Add("Sec-ch-ua-mobile", "?0");
request.Headers.Add("Sec-ch-ua-platform", "\"Windows\"");
request.Headers.Add("Sec-fetch-dest", "empty");
request.Headers.Add("Sec-fetch-mode", "cors");
request.Headers.Add("Sec-fetch-site", "same-site");
request.Headers.Add("User-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
```

## Пример использования

Пример использования приложения для добавления в корзину [данного товара](https://www.wildberries.ru/catalog/170430455/detail.aspx) показан на рисунке.

![image](https://github.com/Gamshik/WildberriesTask/assets/122757204/c14f058c-d5a5-4f0f-b98a-9af1338c801c)


![image](https://github.com/Gamshik/WildberriesTask/assets/122757204/5a99745a-6168-48ad-a02b-88e7ca63fe18)



