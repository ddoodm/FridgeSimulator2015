<?php

mysql_connect("localhost", 			//Server
			  "spinJumpScores",		//User
			  "xXx_t!am420Bla\$\$erz_xXx")			//Pass
			  or die (mysql_error());

//Open the main database:			  
mysql_select_db("spinJumpScores") or die (mysql_error());

?>