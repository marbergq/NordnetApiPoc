﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NordNetApiPoC.NordNetAPI.DataContracts
{

    

    [DataContract]
    public class LoginDataContract
    {
        [DataMember(Name = "country")]
        public string Country { get; private set; }
        [DataMember(Name = "enviroment")]
        public string Enviroment { get; private set; }
        [DataMember(Name = "private_feed")]
        public Feed PrivateFeed { get; set; }
        [DataMember(Name = "public_feed")]
        public Feed PublicFeed { get; set; }
        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; private set; }
        [DataMember(Name = "session_key")]
        public string SessionKey { get; private set; }
    }

    [DataContract]
    public class Feed
    {
        [DataMember(Name = "encrypted")]
        public bool Encrypted { get; set; }
        [DataMember(Name = "hostname")]
        public string HostName { get; set; }
        [DataMember(Name = "port")]
        public int Port { get; set; }

    }


    [DataContract]
    public class Stock
    {
        [DataMember(Name = "shortname")]
        public string ShortName { get; set; }
        [DataMember(Name = "identifier")]
        public string Identifier { get; set; }
        [DataMember(Name = "marketID")]
        public int MarketID { get; set; }

    }

    [DataContract]
    public class HeartBeat
    {
        [DataMember(Name = "logged-in")]
        public Boolean LoggedIn { get; set; }
    }

    [DataContract]
    public class Instrument : Stock
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "isinCode")]
        public string IsinCode { get; set; }
        [DataMember(Name = "longname")]
        public string LongName { get; set; }


    }

    [DataContract]
    public class ChartData
    {
        [DataMember(Name = "timestamp")]
        public string TimeStamp { get; set; }
        [DataMember(Name = "change")]
        public float Change { get; set; }
        [DataMember(Name = "volume")]
        public int Volume { get; set; }
        [DataMember(Name = "price")]
        public float Price { get; set; }
    }

    [DataContract]
    public class Market
    {
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "sortorder")]
        public string SortOrder { get; set; }
    }

    [DataContract]
    public class NewsSource
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "level")]
        public string Level { get; set; }

        [DataMember(Name = "sourceid")]
        public string Sourceid { get; set; }

        [DataMember(Name = "imageurl")]
        public string imageurl { get; set; }

        [DataMember(Name = "countries")]
        public IEnumerable<Country> Countries { get; set; }
    }

    [DataContract]
    public class Country
    {
        [DataMember(Name = "country")]
        public string CountryCode { get; set; }
    }

    [DataContract]
    public class NewsItem
    {
        [DataMember(Name = "datetime")]
        public DateTime Datetime { get; set; }
        [DataMember(Name = "headline")]
        public string HeadLine { get; set; }
        [DataMember(Name = "itemid")]
        public int ItemID { get; set; }
        [DataMember(Name = "sourceid")]
        public int SourceId { get; set; }
        [DataMember(Name = "type")]
        public string type { get; set; }
    }

    [DataContract]
    public class Indices
    {
        [DataMember(Name = "longname")]
        public string LongName { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

    }

    [DataContract]
    public class Account
    {
        [DataMember(Name="id")]
        public string AccountID { get; set; }
        [DataMember(Name="default")]
        public bool Default { get; set; }
    }



    [DataContract]
    public class AccountInfo
    {
        [DataMember(Name = "accountSum")]
        public float AccountSum { get; set; }
        [DataMember(Name = "fullMarketValue")]
        public float FullMarketValue { get; set; }
        [DataMember(Name = "accountCurrency")]
        public string AccountCurrency { get; set; }
        [DataMember(Name = "tradingPower")]
        public float TradingPower { get; set; }
    }

    [DataContract]
    public class AccountPosistions
    { 
        [DataMember(Name="acqPrice")]
        public float AccquirePrice { get; set; }
        [DataMember(Name="acqPriceAcc")]
        public float AcquirePriceInAccountCurrency { get; set; }
        [DataMember(Name="pawnPercent")]
        public float PawnPercent { get; set; }
        [DataMember(Name="qt")]
        public float Quantity { get; set; }
        [DataMember(Name="marketValue")]
        public float MarketValue { get; set; }
        [DataMember(Name="marketValueAcc")]
        public float MarketValueAccountCurrency { get; set; }
        [DataMember(Name = "instrumentID")]
        public AccountPositionsStock InstrumentInfo { get; set; }
    }
    [DataContract]
    public class AccountPositionsStock
    { 
        [DataMember(Name = "mainMarketId")]
        public int MainMarketID { get; set; }
        [DataMember(Name = "identifier")]
        public string Identifier { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "mainMarketPrice")]
        public float? MainMarketPrice { get; set; }        
        
    }

}
