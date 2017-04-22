package nowonbun.data;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;

@WebServlet("/Get")
public class Get extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private Logger logger = LoggerManager.getLogger(this.getClass());

	public Get() {
		super();
	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		try {
			response.setHeader("Content-Type", "text/html;charset=UTF-8");
			String code = request.getParameter("CODE");
			String data = FileCenter.getInstance().readFile(code);
			response.getWriter().print(data);
		} catch (Throwable e) {
			logger.error(request.getRemoteHost());
			logger.error("error");
			logger.error(e);
			throw e;
		}
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		doGet(request, response);
	}
}
