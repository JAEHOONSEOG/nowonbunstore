package service;

import java.util.Map;

import javax.servlet.annotation.WebServlet;
import common.AbstractHttpServlet;

@WebServlet("/Master")
public class Master extends AbstractHttpServlet {

	private static final long serialVersionUID = 1L;

	public Object execute(Map<String,String[]> parameter){
		return null;
	}
}