export class KnightViewDto {
  id!: string

  name!: string

  age!: number

  weapons!: number

  attribute!: string

  attack!: number

  exp!: number

  constructor (id: string) {
    this.id = id
  }

  public static create (id: string): KnightViewDto {
    return new KnightViewDto(id)
  }

  public named (name: string) {
    this.name = name

    return this;
  }

  public withAge (age: number) {
    this.age = age

    return this;
  }

  public withWeaponsQuantity (weapons: number) {
    this.weapons = weapons

    return this;
  }

  public mainAttribute (attr: string) {
    this.attribute = attr

    return this;
  }

  public withAttack (attack: number) {
    this.attack = attack

    return this;
  }

  public experienced (exp: number) {
    this.exp = exp

    return this;
  }
}