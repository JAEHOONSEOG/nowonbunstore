<?php
/**
 * 会員ログイン
 */
include_once $_SERVER ['DOCUMENT_ROOT'] . '/common/AbstractAction.php';
include_once $_SERVER ['DOCUMENT_ROOT'] . '/Dao/MemberDao.php';
include_once $_SERVER ['DOCUMENT_ROOT'] . '/common/DefineCommon.php';
include_once $_SERVER ['DOCUMENT_ROOT'] . '/common/DefineMessage.php';
class LoginAction extends AbstractAction {
	private $errorMsg = null;
	private $pid = null;
	private $pwd = null;
	protected function initialize() {
		if (parent::isPostBack ()) {
			if ($_POST ["pid"] == null) {
				return false;			
			}
			$this->pid = $_POST ["pid"];
			if ($_POST ["pwd"] == null) {
				return false;
			}
			$this->pwd = $_POST ["pwd"];
			return true;
		}
		return false;
	}
	protected function main() {
		$dao = new MemberDao ();
		$rst = $dao->login ( $this->pid, $this->pwd );
		if ($rst == DefineCommon::$LOGIN_ERROR) {
			$this->errorMsg = DefineMessage::$LOGIN_FAILED;
		} else {
			parent::setUserSessionSerialize( $rst );
			parent::redirect ( "/customerPlanList.php" );
		}
	}
	protected function error() {
		if (parent::isPostBack ()) {
			$this->errorMsg = DefineMessage::$LOGIN_FAILED;
		}
	}
	public function isError() {
		return $this->errorMsg != null;
	}
	public function getErrorMsg() {
		return $this->errorMsg;
	}
	public function getUserId() {
		return $this->pid;
	}
}
$obj = new LoginAction ();
$obj->run ();
?>
<!DOCTYPE html>
<html>
<head>
<meta content='text/html; charset=UTF-8' http-equiv='Content-Type' />
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport"
	content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
<script
	src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<link href="css/common.css" rel="stylesheet">
<link href="css/login.css" rel="stylesheet">
<script type="text/javascript" src="js/login.js"></script>
<title><?=$obj->getTitle()?></title>
</head>
<body>
	<header><?=$obj->getHeader()?></header>
	<div class=main>
		<div class="login">
			<form method="post">
				<table class="loginBox">
					<thead>
						<tr>
							<th>ログイン</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td><input type="text" name="pid" id="pid"
								value="<?=$obj->getUserId()?>" placeholder="ID" required
								autofocus autocomplete="off" /></td>
						</tr>
						<tr>
							<td><input type="password" name="pwd" id="pwd"
								placeholder="PASSWORD" required autocomplete="off"></td>
						</tr>
						<?php if($obj->isError()){?>
						<tr>
							<td><span class="errorMsg"><?=$obj->getErrorMsg()?></span></td>
						</tr>
						<?php }?>
						<tr>
							<td><input type="submit" value="ログイン"> <input type="button"
								id="cancel" value="キャンセル"></td>
						</tr>
						<tr>
							<td><a class="link" href="/newApply.php">Sign up</a></td>
					</tbody>
				</table>
			</form>
		</div>
	</div>
	<footer><?=$obj->getFooter()?></footer>
</body>
</html>