function city()
{
var k=0;

var t= document.getElementById("City_name").value
var y=t.length;
for(var i=0;i<y;i++)
{
for(var j="0";j<="9";j++)
{
  if(t.charAt(i)== j)
  {
  k=1;
  break;  
  }
}

}
if(k==0)
{
  return true; 
}
else
{
alert("Sorry,format should not allow any number");
return false;
}


}
