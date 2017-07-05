package jp.co.aisinfo;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import jp.co.aisdata.ITest;
import jp.co.aisinfo.Common.AisCommonHttpServlet;
import jp.co.aisinfo.Common.Define;
import jp.co.aisinfo.Common.Annotation.Allocation;
import jp.co.aisinfo.Util.ArrayUtil;
import jp.co.aisinfo.Util.BitConverter;
import jp.co.aisinfo.Util.StubJson;

@WebServlet("/ReservationNow")
public class ReservationNow extends AisCommonHttpServlet {

	private static final long serialVersionUID = 1L;
	
	@Allocation(ClassName = "jp.co.aisdata.ITestProxy") // TODO C#�`�[�����񋟂���N���X���w��
	private ITest testdao; // TODO C#�`�[�����񋟂���N���X���w��

	@Override
	protected String execute(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		
		// TODO session �����ǉ�
		
		String key = testdao.doWork(); // TODO ���\�b�h�́@getMaster()
		return new jp.co.aisinfo.Util.StubJson().getDataByKey(key, "getMaster");
		//return super.getDataByKey(key);
		//ResultTest result = getClassFromJson(ResultTest.class,data);
		//PrintWriter out = response.getWriter();
	}
	
	
}
