import type { CreateKnightDto } from '@/dto/CreateKnightDto'
import { KnightViewDto } from '@/dto/KnightViewDto'
import type { UpdateKnightNicknameDto } from '@/dto/UpdateKnightNicknameDto'
import axios from 'axios'

export class KnightGateway {
  private readonly host: string = import.meta.env.VITE_WEB_API_URI

  async listKnights () {
    const result = await axios.get(`${this.host}/knights`);

    return result.data.map((item: KnightViewDto) => KnightViewDto.create(item.id)
      .named(item.name)
      .withAge(item.age)
      .withWeaponsQuantity(item.weapons)
      .mainAttribute(item.attribute)
      .withAttack(item.attack)
      .experienced(item.exp))
  }

  async listHeroes () {
    const result = await axios.get(`${this.host}/knights?filter=heroes`);

    return result.data.map((item: KnightViewDto) => KnightViewDto.create(item.id)
      .named(item.name)
      .withAge(item.age)
      .withWeaponsQuantity(item.weapons)
      .mainAttribute(item.attribute)
      .withAttack(item.attack)
      .experienced(item.exp))
  }

  async create (dto: CreateKnightDto) {
    await axios.post(`${this.host}/knights`, dto);
  }

  async updateNickname (knightId: string, dto: UpdateKnightNicknameDto) {
    await axios.patch(`${this.host}/knights/${knightId}`, dto);
  }

  async delete (knightId: string) {
    await axios.delete(`${this.host}/knights/${knightId}`);
  }
}

const knightGateway = new KnightGateway()

export default knightGateway