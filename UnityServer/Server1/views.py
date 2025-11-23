from django.shortcuts import render

# Create your views here.
from django.http import JsonResponse
from django.views.decorators.csrf import csrf_exempt
from django.contrib.auth import authenticate
import json
from django.contrib.auth.models import User

@csrf_exempt
def unity_data(request):
    if request.method == 'POST':
        data = json.loads(request.body)
        print(data)  # 서버 콘솔에서 데이터 확인
        return JsonResponse({"status": "success", "received": data})
    return JsonResponse({"error": "POST 요청만 가능"})

@csrf_exempt
def unity_login(request):
    if request.method == "POST":
        data = json.loads(request.body)
        username = data.get("username")
        password = data.get("password")
        
        user = authenticate(username=username, password=password)
        if user is not None:
            # 로그인 성공
            return JsonResponse({"success": True, "message": "로그인 성공"})
        else:
            return JsonResponse({"error": False, "message": "아이디 또는 비번 틀림"})
    return JsonResponse({"error": False, "message": "POST 요청만 허용"}, status=400)

@csrf_exempt
def unity_register(request):
    if request.method == "POST":
        data = json.loads(request.body)
        username = data.get("username")
        password = data.get("password")

        if User.objects.filter(username=username).exists():
            return JsonResponse({"error": False, "message": "이미 계정이 있습니다."})

        # 새 사용자 생성
        user = User.objects.create_user(username=username, password=password)
        user.save()

        return JsonResponse({"success": True, "message": "회원가입 성공"})
    
    return JsonResponse({"error": False, "message": "POST 요청만 허용"}, status=400)
