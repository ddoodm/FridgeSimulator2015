<?php

// If the socre has been set within a post variable by the name 'score'
if(isset($_POST['score']) && isset($_POST['name']) && isset($_POST['time']))
{
	// Connect to the database
	include_once("dbConnect.php");

	// Strip all but digits (\D) from the input
	$score = mysql_real_escape_string(preg_replace('/\D/', '', $_POST['score']));

	// Strip all but alphanumeric
	$name = mysql_real_escape_string(preg_replace('/[^\da-z]/i', '', $_POST['name']));

	$time = mysql_real_escape_string(preg_replace('/\D/', '', $_POST['time']));

	// Post score
	mysql_query(
		"INSERT INTO spinJumpScores (score, name, time)
		VALUES ('$score', '$name', '$time');");

	print("Insert success. God, I hope you're a computer.");
}
else // Score is not set
	print("<b>Go away, human...</b><br/>That's about as secure as it's going to get with the time I have left.");

?>