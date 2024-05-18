class WeaponDto {
  name: string = ''

  mod: number = 0

  attr: string = ''

  equipped: boolean = false
}

export class AttributesDto {
  strength: number = 0
  
  dexterity: number = 0
  
  constitution: number = 0
  
  intelligence: number = 0
  
  wisdom: number = 0

  charisma: number = 0
}

export class CreateKnightDto {
  name: string = ''

  nickname: string = ''

  birthday: string = ''

  weapons: WeaponDto[] = []

  attributes: AttributesDto = new AttributesDto()

  keyAttribute: string = ''

  addEmptyWeapon () {
    this.weapons.push(
      new WeaponDto()
    )
  }

  removeWeapon (index: number) {
    this.weapons.splice(index, 1)
  }
}