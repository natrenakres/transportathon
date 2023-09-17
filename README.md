# Transportaton App

Proje bir RESTful API, bir MSSQL veritabani ve de bir Single Page Application'dan olusmaktadir. 
Docker compose ile db ve api ayaga kaldirilabilir. 

## Demo icin örnek kullanici bilgileri

1. Member = Normal kullanici Nakliye talebini yapacak olan kisi.
2. Owner = Sirket sahibi, araclari ve ekibi olan kisi, isteklere cevap verip rezervasyon yapacak kisi. 

### Member
 email: member@mail.com
 password: 1q2w3e4r
 
### Owner
email: owner@mail.com
password: 1q2w3e4r

## Kullanim

Backend icin 
```
cd Transportaton
docker-compose up

``` 
komutu ile veritabani ve api calistirilabilir.

Client SPA app icin ise 
```
cd Transportaton.UI/transportaton-web 
```
icinde `npm start` komutu gerekiyor. 