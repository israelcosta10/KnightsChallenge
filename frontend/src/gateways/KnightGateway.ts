import type { CreateKnightDto } from '@/dto/CreateKnightDto'
import { KnightViewDto } from '@/dto/KnightViewDto'
import type { UpdateKnightNicknameDto } from '@/dto/UpdateKnightNicknameDto'
import axios from 'axios'

export class KnightGateway {
  async listKnights () {
    const result = await axios.get("http://localhost:5235/knights");

    return result.data.map((item: KnightViewDto) => KnightViewDto.create(item.id)
      .named(item.name)
      .withAge(item.age)
      .withWeaponsQuantity(item.weapons)
      .mainAttribute(item.attribute)
      .withAttack(item.attack)
      .experienced(item.exp))
  }

  async listHeroes () {
    const result = await axios.get("http://localhost:5235/knights?filter=heroes");

    return result.data.map((item: KnightViewDto) => KnightViewDto.create(item.id)
      .named(item.name)
      .withAge(item.age)
      .withWeaponsQuantity(item.weapons)
      .mainAttribute(item.attribute)
      .withAttack(item.attack)
      .experienced(item.exp))
  }

  async create (dto: CreateKnightDto) {
    await axios.post("http://localhost:5235/knights", dto);
  }

  async updateNickname (knightId: string, dto: UpdateKnightNicknameDto) {
    await axios.patch(`http://localhost:5235/knights/${knightId}`, dto);
  }

  async delete (knightId: string) {
    await axios.delete(`http://localhost:5235/knights/${knightId}`);
  }
}

const knightGateway = new KnightGateway()

export default knightGateway