import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { PrismaService } from './prisma.service';
import { ResultsRepository } from './results-repository';

@Module({
  imports: [],
  controllers: [AppController],
  providers: [AppService, PrismaService, ResultsRepository],
})
export class AppModule {}
