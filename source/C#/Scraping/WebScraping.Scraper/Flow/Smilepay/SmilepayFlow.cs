﻿using System;
using WebScraping.Scraper.Common;
using WebScraping.Scraper.Impl;

namespace WebScraping.Scraper.Flow.Smilepay
{
    class SmilepayFlow : AbstractScrapFlow
    {
        public SmilepayFlow(ScrapBrowser browser, Parameter param)
            : base(browser, param)
        {
            logger.Info("SmilepayFlow initialize");
        }
        protected override void Finally(FlowModelData flowModelData)
        {
            Console.WriteLine("End!");
            Console.ReadLine();
        }
        public override string StartPage(FlowModelData flowModelData)
        {
            return "https://www.smilepay.co.kr/login.do";
        }
        protected override void ScrapType0(FlowModelData flowModelData)
        {

        }
        protected override void ScrapType1(FlowModelData flowModelData)
        {

        }
        protected override void ScrapType2(FlowModelData flowModelData)
        {

        }
        protected override void ScrapType3(FlowModelData flowModelData)
        {

        }
        protected override void ScrapType4(FlowModelData flowModelData)
        {

        }
        protected override void ScrapType5(FlowModelData flowModelData)
        {

        }
    }
}
