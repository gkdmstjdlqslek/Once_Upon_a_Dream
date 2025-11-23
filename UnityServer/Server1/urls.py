from django.urls import path
from . import views

urlpatterns = [
    path('data/', views.unity_data, name='unity_data'),
    path('login/', views.unity_login, name='unity_login'),
    path('register/', views.unity_register, name='unity_register'),

]