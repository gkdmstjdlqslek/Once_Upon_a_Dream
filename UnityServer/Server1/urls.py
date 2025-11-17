from django.urls import path
from . import views

urlpatterns = [
    path('data/', views.unity_data, name='unity_data'),
]