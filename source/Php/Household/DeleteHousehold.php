<?php
include_once $_SERVER ['DOCUMENT_ROOT'] . '/Household/Common/AbstractController.php';
class DeleteHousehold extends AbstractController {
	protected function initialize(){
		
	}
	protected function main(){
		
	}
	protected function validate(){
		
	}
	protected function error($e){
		parent::setHeaderError(406,"");
	}
}
$obj = new DeleteHousehold();
$obj->run();
?>