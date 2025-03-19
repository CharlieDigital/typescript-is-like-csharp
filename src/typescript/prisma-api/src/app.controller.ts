import { Controller, Get, Param } from '@nestjs/common';
import { AppService } from './app.service';
import { RaceResultDto as RunnerRaceResult, ResultsRepository } from './results-repository';
import { Race, RaceResult, Runner } from '@prisma/client';

@Controller()
export class AppController {
  constructor(
    private readonly appService: AppService,
    private readonly resultsRepository: ResultsRepository
  ) {}

  @Get('/top10/:email')
  async getTop10FinishesByRunner(
    @Param('email') email: string
  ): Promise<RunnerRaceResult[]> {
    return await this.resultsRepository.top10FinishesByRunner(email)
  }

  @Get('/results/:email')
  async getRunnerResult (
    @Param('email') email: string
  ) : Promise<RunnerResults> {
    const result = await this.resultsRepository.runnerResults(email)
    return {
      runner: result,
      results: result.races,
      races: result.races.flatMap(r => r.race)
    }
  }
}

type RunnerResults = {
  runner: Runner,
  results: RaceResult[],
  races: Race[]
}
