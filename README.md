# QTP

本平台使用券商API提供即時商品報價，透過開放式的協定讓"報價接收端"能夠不必綁定於Windows平台，
"報價提供端"採用RabbitMQ建立Topic Exchange的機制將資料推播到訂閱商品資料的節點，
同時利用MongoDB提供Tick Level歷史資料的存取。主要讓使用Python, Java及C#語言的策略開發者能快速進入策略的開發及測試。

## 報價提供端
1. Windows的環境
2. 需有群益API的權限

## 報價接受端
1. 即時接收Tick資料介面
  * https://www.rabbitmq.com/tutorials/tutorial-five-python.html
  * 主要支援語言(Python, Java, Ruby, PHP, C#, Javascript, Go)
2. 資料存取介面
  * https://api.mongodb.org/python/current/
  * 主要支援語言(Python, Java, C#, C++)
