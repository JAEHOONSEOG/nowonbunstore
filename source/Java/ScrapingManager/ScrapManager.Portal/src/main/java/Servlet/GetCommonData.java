package Servlet;

import javax.servlet.annotation.WebServlet;
import org.apache.log4j.Logger;
import Common.AjaxServlet;
import Common.LoggerManager;
import Common.Util;
import common.FactoryDao;
import dao.CommonDataDao;
import dao.KeyNodeDao;

@WebServlet("/GetCommonData")
public class GetCommonData extends AjaxServlet {
	private static final long serialVersionUID = 1L;
	private Logger logger = LoggerManager.getLogger(GetCommonData.class);

	@Override
	protected String doAjax() {
		String key = getParameter("KEY");
		logger.info("[WEB LOG] PARAMETER - " + key);
		if (Util.StringIsNullEmpty(key)) {
			setStatus(401);
			return "";
		}
		int mallkey = FactoryDao.getDao(KeyNodeDao.class).getMallkey(key);
		return getJsonData(FactoryDao.getDao(CommonDataDao.class).selectByKey(mallkey, key));
	}

}
