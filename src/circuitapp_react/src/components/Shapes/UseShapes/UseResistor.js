import React, {Component} from 'react';
import PropTypes from "prop-types";

/**
 * Компонент для использования шаблона резистора.
 */
class UseResistor extends Component {
    constructor(props) {
        super(props);
        this.state = {
            X: this.props.x,
            Y: this.props.y,
            width: this.props.width,
            height: this.props.height,
            rotate: 0
        }
        this.setCoordinate = this.setCoordinate.bind(this);
        this.changeRotate = this.changeRotate.bind(this);
    }
    
    setCoordinate(x, y) {
        this.setState({ X: x, Y: y });
    }
    
    changeRotate(){
        let oldRotate = this.state.rotate;
        this.setState({ rotate: (oldRotate + 90) % 360})
    }
    
    render() {
        return (
            <use
                x={this.state.X} 
                y={this.state.Y} 
                href={this.props.href} 
                className='draggable' 
                style={{ cursor: this.props.canNotDraggable ? 'default' : 'move' }}
                stroke="black"
                id={this.props.id}
                transform={`rotate(${this.state.rotate} ${this.state.X+this.state.width/2} ${this.state.Y+this.state.height/2})`}
            />
        );
    }
}
UseResistor.propTypes = {
    x: PropTypes.number,
    y: PropTypes.number,
    href: PropTypes.string.isRequired,
    canNotDraggable: PropTypes.bool
}
UseResistor.defaultProps = {
    x: 0,
    y: 0,
    canNotDraggable: false
}

export default UseResistor;