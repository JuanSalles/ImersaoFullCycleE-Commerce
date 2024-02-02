import { Module } from '@nestjs/common';
import { AuthService } from './auth.service';
import { AuthController } from './auth.controller';
import { JwtModule } from '@nestjs/jwt';
@Module({
  imports: [
    JwtModule.register({
      signOptions: { expiresIn: '60s' },
      secret:
        'ad36905a3776872def08aef61e4eccfa3e15c31033e089b0fb91e90c47aefbc821c244a4f32cc3f3842a95364d8e25526975d26fb96b60ab4da873644f791147',
    }),
  ],
  controllers: [AuthController],
  providers: [AuthService],
})
export class AuthModule {}
