﻿using System;
using System.Collections.Generic;
using WebScraping.Scraper.Common;
using Gecko;
using Gecko.DOM;
using WebScraping.Scraper.Other;
using Newtonsoft.Json;
using System.Text;


namespace WebScraping.Scraper.Flow._11thStreet
{
    partial class StreetFlow : AbstractScrapFlow
    {
        private StringBuilder buffer = new StringBuilder();
        private List<String> statsPeriodProdSelSummaryList = new List<string>();
        private int index;
        private DateTime std;

        private void Login(FlowModelData data)
        {
            if (data.Uri.ToString().IndexOf("returnURL") != -1)
            {
                SetCommonData(0, "FALSE");
            }
            data.Document.GetElementById<GeckoInputElement>("loginName").Value = Parameter.Id1;
            data.Document.GetElementById<GeckoInputElement>("passWord").Value = Parameter.Pw1;
            data.Document.GetElementByClassName<GeckoInputElement>("btn_login").Click();
        }
        private void Index(FlowModelData data)
        {
            SetCommonData(0, "TRUE");
            var data26 = data.Document.GetElementById<GeckoElement>("prdQnaAnswerCnt").FirstChild.NodeValue;
            SetCommonData(26, data26);
            var data25 = data.Document.GetElementById<GeckoElement>("emergencyCnt").FirstChild.NodeValue;
            SetCommonData(25, data25);
            base.Navigate("http://soffice.11st.co.kr/marketing/SellerMenuAction.tmall?method=getSellerMainNew&usedClfCd=00&memSuplCmpClfCd=01");
        }
        private void SellerMenuAction(FlowModelData data)
        {
            var data02 = data.Document.GetElementById<GeckoElement>("sellerGradePop").ParentElement.GetElementByTagName<GeckoElement>("A").TextContent;
            SetCommonData(2, data02);
            base.Navigate("https://soffice.11st.co.kr/register/SellerInfoEdit.tmall?method=sellerInfoEdit&orgMenuNo=5002");
        }
        private void SellerInfoEdit(FlowModelData data)
        {
            FlowList["register/SellerInfoEdit.tmall"] = new Common.Flow
            {
                Key = "register/SellerInfoEdit.tmall",
                Func = SellerInfoEdit2,
                Next = ""
            };

            data.Document.GetElementById<GeckoInputElement>("mem_pwd").Value = Parameter.Pw1;
            data.Document.GetElementByClassName<GeckoAnchorElement>("xladtype").Click();
        }
        private void SellerInfoEdit2(FlowModelData data)
        {
            var table = data.Document.SelectTableByClass("def_tableA");
            SetCommonData(1, table.Get(0, 1).TextContent);
            SetCommonData(4, table.Get(0, 3).TextContent);
            SetCommonData(3, table.Get(5, 1).TextContent);
            SetCommonData(5, table.Get(5, 3).TextContent);
            SetCommonData(8, table.Get(6, 1).TextContent);
            SetCommonData(7, table.Get(6, 3).TextContent);
            SetCommonData(6, table.Get(7, 1).TextContent);
            this.buffer.Clear();
            this.buffer.Append(data.Document.GetElementByName<GeckoSelectElement>("rptvTlphnNO1").Value).Append("-");
            this.buffer.Append(data.Document.GetElementByName<GeckoInputElement>("rptvTlphnNO2").Value).Append("-");
            this.buffer.Append(data.Document.GetElementByName<GeckoInputElement>("rptvTlphnNO3").Value);
            SetCommonData(9, this.buffer.ToString());

            this.buffer.Clear();
            this.buffer.Append(data.Document.GetElementByName<GeckoSelectElement>("hotlinePrtblTlphnNO1").Value).Append("-");
            this.buffer.Append(data.Document.GetElementByName<GeckoInputElement>("hotlinePrtblTlphnNO2").Value).Append("-");
            this.buffer.Append(data.Document.GetElementByName<GeckoInputElement>("hotlinePrtblTlphnNO3").Value);
            SetCommonData(10, this.buffer.ToString());

            this.buffer.Clear();
            this.buffer.Append(data.Document.GetElementById<GeckoInputElement>("id_email").Value).Append("@");
            var value = data.Document.GetElementById<GeckoSelectElement>("id_email_type").Value;
            foreach (var t in data.Document.GetElementById<GeckoSelectElement>("id_email_type").GetElementsByTagName("OPTION"))
            {
                if ((t as GeckoOptionElement).Value.Equals(value))
                {
                    this.buffer.Append(t.TextContent);
                    break;
                }
            }
            SetCommonData(13, this.buffer.ToString());

            this.buffer.Clear();
            this.buffer.Append(data.Document.GetElementByName<GeckoSelectElement>("faxNO1").Value).Append("-");
            this.buffer.Append(data.Document.GetElementByName<GeckoInputElement>("faxNO2").Value).Append("-");
            this.buffer.Append(data.Document.GetElementByName<GeckoInputElement>("faxNO3").Value);
            SetCommonData(11, this.buffer.ToString());
            SetCommonData(18, data.Document.GetElementById<GeckoInputElement>("addrVisit").Value);

            var temp = table.Get(29, 1);
            var temp1 = temp.GetElementsByTagName("INPUT");
            int pos = 0;
            foreach (var t in temp1)
            {
                if ((t as GeckoInputElement).Checked)
                {
                    break;
                }
                pos++;
            }
            SetCommonData(27, temp.GetElementsByTagName("LABEL")[pos].TextContent);
            SetCommonData(16, table.Get(30, 1).TextContent);
            this.buffer.Clear();

            std = DateTime.Now.AddYears(-3).AddDays(DateTime.Now.Day * -1).AddDays(1);
            DateTime etd = std.AddMonths(1).AddDays(-1);
            index = 0;
            this.buffer.Append("http://soffice.11st.co.kr/stats/StatsPeriodProdSel.tmall?");
            this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method","getStatsPeriodProdSelList"},
                        {"start","0" },
                        {"limit","10" },
                        {"stDate",std.ToString("yyyy/MM/dd").Replace("-","/") },
                        {"edDate",std.ToString("yyyy/MM/dd").Replace("-","/") },
                        {"stDatePre", std.ToString("yyyy/MM/dd").Replace("-","/")},
                        {"edDatePre",etd.ToString("yyyy/MM/dd").Replace("-","/") },
                        {"dtPeriod", "PRE_THREE_MONTH"},
                        {"dispGb","grid" }
                    }));
            //PostAjaxJson(document, this.buffer.ToString(), new Dictionary<String, Object>() { });
            base.Navigate(this.buffer.ToString());
        }
        private void StatsPeriodProdSel(FlowModelData data)
        {
            String json = data.Document.Body.TextContent;
            StatsPeriodProdSelJson obj = JsonConvert.DeserializeObject<StatsPeriodProdSelJson>(json);
            var node = obj.statsPeriodProdSelSummary[0];
            SetPackageData(0, index++, JsonConvert.SerializeObject(node));
            if (std.Year == DateTime.Now.Year && std.Month == DateTime.Now.Month)
            {

                std = DateTime.Now.AddYears(-3).AddDays(DateTime.Now.Day * -1).AddDays(1);
                DateTime etd1 = std.AddMonths(1).AddDays(-1);
                var interval = DateTime.Now - std;
                index = 0;
                this.buffer.Clear();
                this.buffer.Append("http://soffice.11st.co.kr/remittance/SellerRemittanceAction.tmall?");
                this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method","getSellerOwnSelStatSoffice"},
                        {"dtlSearchStlmntType","N" },
                        {"searchDtType","BUY_CNFRM_DT" },
                        {"stDate",std.ToString("yyyyMMdd") },
                        {"edDate",std.ToString("yyyyMMdd") },
                        {"dtlSearchType", ""},
                        {"ordPrdStat","" },
                        {"intervalDay", interval.Days.ToString()},
                        {"dtlSearchVal","" }
                    }));
                base.Navigate(this.buffer.ToString());
            }
            std = std.AddMonths(1);
            DateTime etd = std.AddMonths(1).AddDays(-1);
            if (std.Year == DateTime.Now.Year && std.Month == DateTime.Now.Month)
            {
                etd = DateTime.Now;
            }
            this.buffer.Clear();
            this.buffer.Append("http://soffice.11st.co.kr/stats/StatsPeriodProdSel.tmall?");
            this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method","getStatsPeriodProdSelList"},
                        {"start","0" },
                        {"limit","10" },
                        {"stDate",std.ToString("yyyy/MM/dd").Replace("-","/") },
                        {"edDate",etd.ToString("yyyy/MM/dd").Replace("-","/") },
                        {"stDatePre", std.ToString("yyyy/MM/dd").Replace("-","/")},
                        {"edDatePre",etd.ToString("yyyy/MM/dd").Replace("-","/") },
                        {"dtPeriod", "PRE_THREE_MONTH"},
                        {"dispGb","grid" }
                    }));
            base.Navigate(this.buffer.ToString());
        }

        /// <summary>
        /// 4-2. 정산내역 -> 판매정산 현황
        /// </summary>
        /// <param name="document"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private void SellerRemittanceAction(FlowModelData data)
        {
            String json = data.Document.Body.TextContent;
            SellerRemittanceActionJson obj = JsonConvert.DeserializeObject<SellerRemittanceActionJson>(json);
            var node = obj.list[0];
            SetPackageData(1, index++, JsonConvert.SerializeObject(node));
            if (std.Year == DateTime.Now.Year && std.Month == DateTime.Now.Month)
            {
                this.buffer.Clear();
                this.buffer.Append("https://soffice.11st.co.kr/escrow/OrderingLogisticsAction.tmall?");
                this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method", "getOrderLogisticsList"},
                        {"listType", "orderingLogistics" },
                        {"start", "0" },
                        {"limit", "10000" },
                        {"shDateType", "02" },
                        {"shDateFrom", DateTime.Now.AddYears(-1).ToString("yyyyMMdd")},
                        {"shDateTo", DateTime.Now.ToString("yyyyMMdd") },
                        {"shBuyerType", "" },
                        {"shBuyerText", "" },
                        {"shProductStat","301" },
                        {"shDelayReport","" },
                        {"shPurchaseConfirm","" },
                        {"shGblDlv","N" },
                        {"prdNo","" },
                        {"shStckNo","" },
                        {"shOrderType","on"},
                        {"addrSeq","" },
                        {"isAbrdSellerYn","" },
                        {"abrdOrdPrdStat","" },
                        {"isItalyAgencyYn","" },
                        {"shErrYN","" },
                        {"gblRcvrNm","" },
                        {"gblRcvrMailNo","" },
                        {"gblRcvrBaseAddr","" },
                        {"gblRcvrDtlsAddr","" },
                        {"gblRcvrTlphn","" },
                        {"gblRcvrPrtblNo","" },
                        {"shOrdLang","" },
                        {"shDlvClfCd","" }
                    }));
                base.Navigate(this.buffer.ToString());
            }
            std = std.AddMonths(1);
            DateTime etd = std.AddMonths(1).AddDays(-1);
            var interval = DateTime.Now - std;
            this.buffer.Clear();
            this.buffer.Append("http://soffice.11st.co.kr/remittance/SellerRemittanceAction.tmall?");
            this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method","getSellerOwnSelStatSoffice"},
                        {"dtlSearchStlmntType","N" },
                        {"searchDtType","BUY_CNFRM_DT" },
                        {"stDate",std.ToString("yyyyMMdd") },
                        {"edDate",etd.ToString("yyyyMMdd") },
                        {"dtlSearchType", ""},
                        {"ordPrdStat","" },
                        {"intervalDay", interval.Days.ToString()},
                        {"dtlSearchVal","" }
                    }));
            base.Navigate(this.buffer.ToString());
        }

        /// <summary>
        /// 5-1. 정산예정금 (주문관리 → 배송준비중)
        /// </summary>
        /// <param name="document"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private void OrderingLogisticsAction(FlowModelData data)
        {
            String json = data.Document.Body.TextContent;
            OrderingLogisticsActionJson obj = JsonConvert.DeserializeObject<OrderingLogisticsActionJson>(json);
            index = 0;
            obj.orderingLogistics.ForEach(node =>
            {
                SetPackageData(2, index++, JsonConvert.SerializeObject(node));
            });
            FlowList["escrow/OrderingLogisticsAction.tmall"] = new Common.Flow
            {
                Func = OrderingLogisticsAction2
            };
            this.buffer.Clear();
            this.buffer.Append("https://soffice.11st.co.kr/escrow/OrderingLogisticsAction.tmall?");
            this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method", "getOrderLogisticsList"},
                        {"listType", "orderingLogistics" },
                        {"start", "0" },
                        {"limit", "10000" },
                        {"shDateType", "02" },
                        {"shDateFrom", DateTime.Now.AddYears(-1).ToString("yyyyMMdd")},
                        {"shDateTo", DateTime.Now.ToString("yyyyMMdd") },
                        {"shBuyerType", "" },
                        {"shBuyerText", "" },
                        {"shProductStat","401" },
                        {"shDelayReport","" },
                        {"shPurchaseConfirm","" },
                        {"shGblDlv","N" },
                        {"prdNo","" },
                        {"shStckNo","" },
                        {"shOrderType","on"},
                        {"addrSeq","" },
                        {"isAbrdSellerYn","" },
                        {"abrdOrdPrdStat","" },
                        {"isItalyAgencyYn","" },
                        {"shErrYN","" },
                        {"gblRcvrNm","" },
                        {"gblRcvrMailNo","" },
                        {"gblRcvrBaseAddr","" },
                        {"gblRcvrDtlsAddr","" },
                        {"gblRcvrTlphn","" },
                        {"gblRcvrPrtblNo","" },
                        {"shOrdLang","" },
                        {"shDlvClfCd","" }
                    }));
            base.Navigate(this.buffer.ToString());
        }

        /// <summary>
        /// 5-2. 정산예정금 (배송관리-배송중)
        /// </summary>
        /// <param name="document"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private void OrderingLogisticsAction2(FlowModelData data)
        {
            String json = data.Document.Body.TextContent;
            OrderingLogisticsActionJson obj = JsonConvert.DeserializeObject<OrderingLogisticsActionJson>(json);
            index = 0;
            obj.orderingLogistics.ForEach(node =>
            {
                SetPackageData(3, index++, JsonConvert.SerializeObject(node));
            });
            FlowList["escrow/OrderingLogisticsAction.tmall"] = new Common.Flow
            {
                Func = OrderingLogisticsAction3
            };
            this.buffer.Clear();
            this.buffer.Append("https://soffice.11st.co.kr/escrow/OrderingLogisticsAction.tmall?");
            this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method", "getOrderLogisticsList"},
                        {"listType", "orderingLogistics" },
                        {"start", "0" },
                        {"limit", "10000" },
                        {"shDateType", "02" },
                        {"shDateFrom", DateTime.Now.AddYears(-1).ToString("yyyyMMdd")},
                        {"shDateTo", DateTime.Now.ToString("yyyyMMdd") },
                        {"shBuyerType", "" },
                        {"shBuyerText", "" },
                        {"shProductStat","501" },
                        {"shDelayReport","" },
                        {"shPurchaseConfirm","" },
                        {"shGblDlv","N" },
                        {"prdNo","" },
                        {"shStckNo","" },
                        {"shOrderType","on"},
                        {"addrSeq","" },
                        {"isAbrdSellerYn","" },
                        {"abrdOrdPrdStat","" },
                        {"isItalyAgencyYn","" },
                        {"shErrYN","" },
                        {"gblRcvrNm","" },
                        {"gblRcvrMailNo","" },
                        {"gblRcvrBaseAddr","" },
                        {"gblRcvrDtlsAddr","" },
                        {"gblRcvrTlphn","" },
                        {"gblRcvrPrtblNo","" },
                        {"shOrdLang","" },
                        {"shDlvClfCd","" }
                    }));
            base.Navigate(this.buffer.ToString());
        }
        /// <summary>
        /// 5-3. 정산예정금 (배송관리 →배송완료 )
        /// </summary>
        /// <param name="document"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private void OrderingLogisticsAction3(FlowModelData data)
        {
            String json = data.Document.Body.TextContent;
            OrderingLogisticsActionJson obj = JsonConvert.DeserializeObject<OrderingLogisticsActionJson>(json);
            index = 0;
            obj.orderingLogistics.ForEach(node =>
            {
                SetPackageData(4, index++, JsonConvert.SerializeObject(node));
            });
            FlowList["remittance/SellerRemittanceAction.tmall"] = new Common.Flow
            {
                Func = SellerRemittanceAction2
            };
            base.Navigate("http://soffice.11st.co.kr/remittance/SellerRemittanceAction.tmall?method=displaySellerOwnSelStatSoffice");
        }
        private void SellerRemittanceAction2(FlowModelData data)
        {
            String date = data.Document.GetElementByClassName<GeckoHtmlElement>("update").TextContent;
            int pos = date.IndexOf("20");
            date = date.Substring(pos, 4) + date.Substring(pos + 5, 2) + date.Substring(pos + 8, 2);
            logger.Debug(date);
            DateTime etd;
            if (DateTime.Now.ToString("yyyyMMdd").Equals(date))
            {
                std = DateTime.Now.AddDays(-1);
                etd = DateTime.Now.AddDays(-1);
            }
            else
            {
                std = DateTime.Now.AddDays(-2);
                etd = DateTime.Now.AddDays(-1);
            }
            FlowList["remittance/SellerRemittanceAction.tmall"] = new Common.Flow
            {
                Func = SellerRemittanceAction3
            };
            this.buffer.Clear();
            this.buffer.Append("http://soffice.11st.co.kr/remittance/SellerRemittanceAction.tmall?");
            this.buffer.Append(CreateGetParameter(new Dictionary<String, String>()
                    {
                        {"method","getSellerOwnSelStatSoffice"},
                        {"dtlSearchStlmntType","Y" },
                        {"searchDtType","BUY_CNFRM_DT" },
                        {"stDate",std.ToString("yyyyMMdd") },
                        {"edDate",etd.ToString("yyyyMMdd") },
                        {"dtlSearchType", ""},
                        {"ordPrdStat","" },
                        {"intervalDay", "0"},
                        {"dtlSearchVal","" }
                    }));
            base.Navigate(this.buffer.ToString());
        }
        /// <summary>
        /// 5-4. 정산예정금 ( 배송관리 →구매확정) 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private void SellerRemittanceAction3(FlowModelData data)
        {
            String json = data.Document.Body.TextContent;
            SellerRemittanceActionJson obj = JsonConvert.DeserializeObject<SellerRemittanceActionJson>(json);
            var node = obj.list[0];
            SetPackageData(5, 0, JsonConvert.SerializeObject(node));
        }
    }
}
