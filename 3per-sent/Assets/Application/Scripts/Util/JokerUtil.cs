public class JokerUtil {
	public static string GetNextJokerScene(string a){
		if (a == "stage1") {
			return "wide/scene2";
		}else if(a == "stage2"){
			return "wide/scene3";
		}else if(a == "stage3"){
			return "wide/scene4";
		}
		return null;
	}

}
