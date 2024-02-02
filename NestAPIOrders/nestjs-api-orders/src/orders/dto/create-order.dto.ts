import { Type } from 'class-transformer';
import {
  ArrayNotEmpty,
  IsInt,
  IsNotEmpty,
  IsPositive,
  IsString,
  IsUUID,
  MaxLength,
  ValidateNested,
} from 'class-validator';

export class CreateOrderDto {
  @ArrayNotEmpty()
  @ValidateNested({ each: true })
  @Type(() => OrderItemsDto)
  items: OrderItemsDto[];

  @MaxLength(255)
  @IsString()
  @IsNotEmpty()
  card_hash: string;

  client_id: number = 1;
}

export class OrderItemsDto {
  @IsUUID()
  @IsString()
  @IsNotEmpty()
  product_id: string;
  @IsPositive()
  @IsInt()
  @IsNotEmpty()
  quantity: number;
}
