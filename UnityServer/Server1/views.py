from django.shortcuts import render

# Create your views here.
from django.http import JsonResponse
from django.views.decorators.csrf import csrf_exempt
import json

@csrf_exempt
def unity_data(request):
    if request.method == 'POST':
        data = json.loads(request.body)
        print(data)  # 서버 콘솔에서 데이터 확인
        return JsonResponse({"status": "success", "received": data})
    return JsonResponse({"error": "POST 요청만 가능"})