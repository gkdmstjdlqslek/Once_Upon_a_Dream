"""
ASGI config for UnityServer project.

It exposes the ASGI callable as a module-level variable named ``application``.

For more information on this file, see
https://docs.djangoproject.com/en/5.2/howto/deployment/asgi/
"""

import os
from channels.routing import ProtocolTypeRouter, URLRouter
from channels.auth import AuthMiddlewareStack
from django.core.asgi import get_asgi_application
from Server1 import routing  # 앱 이름이 Server1이므로 이렇게

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'UnityServer.settings')

application = ProtocolTypeRouter({
    "http": get_asgi_application(),  # 기존 HTTP 처리
    "websocket": AuthMiddlewareStack(
        URLRouter(
            routing.websocket_urlpatterns  # WebSocket URL 패턴
        )
    ),
})

