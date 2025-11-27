import json
from channels.generic.websocket import AsyncWebsocketConsumer

class GameConsumer(AsyncWebsocketConsumer):
    async def connect(self):
        # 클라이언트에서 match_id를 쿼리스트링으로 보내온다고 가정
        self.match_id = self.scope['url_route']['kwargs']['match_id']
        self.room_name = f"match_{self.match_id}"

        # 매치별 그룹에 참가
        await self.channel_layer.group_add(self.room_name, self.channel_name)
        await self.accept()

    async def disconnect(self, close_code):
        # 그룹에서 나가기
        await self.channel_layer.group_discard(self.room_name, self.channel_name)

    async def receive(self, text_data):
        data = json.loads(text_data)

        # 매치 그룹에게만 메시지 전송
        await self.channel_layer.group_send(
            self.room_name,
            {
                "type": "game_message",
                "payload": data
            }
        )

    async def game_message(self, event):
        # 받은 데이터 그대로 해당 그룹 클라이언트에게 전달
        await self.send(text_data=json.dumps(event["payload"]))
