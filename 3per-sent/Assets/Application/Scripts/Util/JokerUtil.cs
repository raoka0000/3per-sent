public class JokerUtil {
	public static string GetNextJokerScene(string a){
		if (a == "sansi") {
			return "wide/scene1";
		}else if(a == "stage1"){
			return "wide/scene2";
		}
		return null;
	}

}
