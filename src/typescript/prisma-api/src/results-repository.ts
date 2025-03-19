import { Injectable } from "@nestjs/common";
import { PrismaService } from "./prisma.service";

@Injectable()
export class ResultsRepository {
  constructor(private readonly prismaService: PrismaService) {}

  async top10FinishesByRunner(email: string) : Promise<RaceResultDto[]> {
    return (await this.prismaService.raceResult.findMany({
      select:{
        runner: { select: { name: true } },
        race: { select: { name: true, date: true } },
        position: true,
        time: true
      },
      where: {
        runner: { email: 'ada@example.org' },
        position: { lte: 10 }
      },
      orderBy: { position: 'asc' }
    })).map(r => ({
      runnerName: r.runner.name,
      raceName: r.race.name,
      position: r.position,
      time: r.time,
      raceDate: r.race.date
    }))
  }

  async runnerResults(email: string) {
    return await this.prismaService.runner.findFirst({
      where: {
        email: 'ada@example.org'
      },
      include: {
        races: { include: { race: true } }
      }
    })
  }
}

export type RaceResultDto = {
  runnerName: string,
  raceName: string,
  position: number,
  time: number,
  raceDate: Date
}
