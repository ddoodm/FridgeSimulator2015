<?php

// Connect to the database
include_once("dbConnect.php");

// Show top 10 socres
$query = "SELECT score, name, time FROM spinJumpScores ORDER BY score DESC LIMIT 10";

$result = mysql_query($query);

while ($row = mysql_fetch_assoc($result))
	print(sprintf("%s %s %s<br/>", $row['score'], $row['name'], $row['time']));

mysql_free_result($result);

?>