import { Controller, Get, Param } from '@nestjs/common';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  getHello(): string {
    return this.appService.getHello();
  }

  @Get('/echo/:message')
  echo(@Param("message") message: string): string {
    return `You said: ${message}`;
  }

  @Get('/increment/:count')
  increment(@Param("count") count: number): string {
    return `You added: ${count}`;
  }
}
