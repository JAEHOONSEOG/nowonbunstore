package web.only1.Main.Risk;

import javax.servlet.annotation.WebServlet;

import web.only1.Common.AbstractLoadServlect;
import web.only1.Common.View;
import web.only1.Common.ViewResult;

/**
 * Servlet implementation class RishHistory
 */
@WebServlet("/RishHistory")
public class RishHistory extends AbstractLoadServlect {
	private static final long serialVersionUID = 1L;

	public RishHistory() {
		super();
	}

	@Override
	protected ViewResult execute() {
		return View.nativeCode("대손 채권 현황");
	}
}
